<template>
  <div class="login-container">
    <div class="login-content">
      <h2 class="login-content-header">{{ this.$_MSResource[this.$_LANG_CODE].LOGIN.LoginText.toUpperCase() }}</h2>
      <div class="login-content-center">
        <div class="full-content-custom">
          <label> {{ this.$_MSResource[this.$_LANG_CODE].LOGIN.UserName }}:</label>
          <div class="container-input">
            <ms-input
              ref="user_name"
              v-model="user.user_name"
              :class="{ 'border-red': isBorderRed.user_name }"
              @input="setIsBorderRed('user_name')"
              @mouseenter="isHovering.user_name = true"
              @mouseleave="isHovering.user_name = false"
              :maxLength="50"
              @keydown.enter="handleLogin"
            ></ms-input>
            <div class="ms-tooltip" v-if="isHovering.user_name && isBorderRed.user_name">
              {{ errors["user_name"] }}
            </div>
          </div>
        </div>
        <div class="full-content-custom">
          <label> {{ this.$_MSResource[this.$_LANG_CODE].LOGIN.PassWord }}:</label>
          <div class="container-input">
            <ms-input
              ref="pass_word"
              v-model="user.pass_word"
              :class="{ 'border-red': isBorderRed.pass_word }"
              @input="setIsBorderRed('pass_word')"
              @mouseenter="isHovering.pass_word = true"
              @mouseleave="isHovering.pass_word = false"
              :maxLength="50"
              :typeValue="'password'"
              @keydown.enter="handleLogin"
            ></ms-input>
            <div class="ms-tooltip" v-if="isHovering.pass_word && isBorderRed.pass_word">
              {{ errors["pass_word"] }}
            </div>
          </div>
        </div>
        <div class="login-content-footer">
          <ms-button-default
            @click="handleLogin"
            :textButtonDefault="$_MSResource[$_LANG_CODE].LOGIN.LoginText"
          ></ms-button-default>
        </div>
      </div>
    </div>
    <!-- dialog student input data not blank -->
    <ms-dialog-data-not-null
      v-if="isShowDialogDataNotNull"
      :valueNotNull="dataNotNull"
      :title="this.$_MSResource[this.$_LANG_CODE].DIALOG.TITLE.LOGIN_FAILED"
    ></ms-dialog-data-not-null>
    <img
      v-show="isShowLoading"
      class="loading-custom"
      :class="{ 'loadding-form-detail': isShowFormDetail }"
      src="../../assets/img/loading.svg"
      alt="loading"
    />
  </div>
</template>

<script>
import helperCommon from "@/helpers/helper";
import authService from "@/services/auth.js";

export default {
  name: "LoginPage",
  data() {
    return {
      user: {
        user_name: "",
        pass_word: "",
      },
      userProperty: ["user_name", "pass_word"],
      // Khai báo biến chứa danh sách đối tượng lỗi
      errors: {},
      // Khai báo biến chứa danh sách các ô input khi hover
      isHovering: {},
      // Khai báo biến xác định border red
      isBorderRed: {},
      // Khai báo biến xác định nội dung trường nào k được để trống
      dataNotNull: [],
      isShowDialogDataNotNull: false,
      isShowLoading: false,
    };
  },
  created() {
    this.$_MSEmitter.on("closeDialogDataError", () => {
      this.onCloseDialog();
    });
  },
  mounted() {
    if (this.$refs.user_name) {
      this.$refs.user_name.focus();
    }
  },
  methods: {
    async handleLogin() {
      try {
        this.validateLogin();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          this.isShowLoading = true;
          let res = await authService.login(this.user);
          this.isShowLoading = false;
          if (res && res.data && this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code)) {
            if (res.data.Status === 1) {
              sessionStorage.setItem("token", res.data.Token);
              const permission = parseInt(res.data.Permission);
              sessionStorage.setItem("permission", permission);

              switch (permission) {
                case this.$_MSEnum.PERMISSION.Admin:
                  this.$router.push("/management-student");
                  break;
                case this.$_MSEnum.PERMISSION.Student:
                  this.$router.push("/management-score");
                  break;
                case this.$_MSEnum.PERMISSION.Teacher:
                  this.$router.push("/management-score");
                  break;
                default:
                  this.$router.push("/login");
                  break;
              }
            } else {
              this.dataNotNull.push("Tài khoản này đã bị ngưng sử dụng");
              this.isShowDialogDataNotNull = true;
            }
          } else {
            this.dataNotNull.push("Tên đăng nhập hoặc mật khẩu không đúng");
            this.isShowDialogDataNotNull = true;
          }
        }
      } catch (error) {
        console.log(error);
        this.isShowLoading = false;
      }
    },
    /**
     * Mô tả: Validate lỗi nhập liệu phía fontend
     * created by : BNTIEN
     * created date: 11-07-2023 10:07:22
     */
    validateLogin() {
      try {
        for (const refInput of this.userProperty) {
          switch (refInput) {
            case "user_name":
            case "pass_word":
              if (helperCommon.isEmptyInput(this.user[refInput])) {
                this.setError(refInput);
              }
              break;
            default:
              break;
          }
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm set các lỗi nhập liệu phía fontend
     * created by : BNTIEN
     * created date: 11-07-2023 16:36:05
     */
    setError(key) {
      try {
        this.errors[key] = this.$_MSResource[this.$_LANG_CODE].VALIDATE[key];
        this.isBorderRed[key] = true;
        this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].VALIDATE[key]);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm bỏ border red khi dữ liệu thay đổi
     * created by : BNTIEN
     * created date: 29-06-2023 22:03:38
     */
    setIsBorderRed(prop) {
      if (prop in this.isBorderRed) {
        this.isBorderRed[prop] = false;
      }
    },
    /**
     * Mô tả: Hàm đóng dialog cảnh báo dữ liệu k được để trống và focus vào các ô trống
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:59
     */
    onCloseDialog() {
      this.isShowDialogDataNotNull = false;
      this.dataNotNull = [];
      let listPropError = [];
      for (const key in this.isBorderRed) {
        if (this.isBorderRed[key] === true) {
          listPropError.push(key);
        }
      }
      for (const prop of this.userProperty) {
        if (listPropError.includes(prop)) {
          this.$nextTick(() => {
            this.$refs[prop].focus();
          });
          return;
        }
      }
    },
  },
  beforeUnmount() {
    this.$_MSEmitter.off("closeDialogDataError");
  },
};
</script>

<style scoped>
@import url(./LoginPage.css);
@import url(@/css/detailinfo.css);

.overlay-dialog {
  background-color: rgb(197, 206, 203);
  position: absolute;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  z-index: 9000;
}

.loading-custom {
  position: fixed;
  top: 50%;
  left: 49%;
  animation: spin 1s infinite linear;
  z-index: 9999;
}
</style>
