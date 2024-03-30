<template>
  <div id="detail-info-user" class="position-display-center" ref="FormDetail">
    <div class="form-detail-toolbar">
      <div class="question-icon icon-tb" :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.HELP"></div>
      <div
        @click="onCloseFormDetail"
        class="close-icon icon-tb"
        id="teacher-exist"
        @keydown.tab.prevent="resetTab($event.target.value)"
        :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.EXIST"
      ></div>
    </div>
    <div class="form-detail-main">
      <div class="teacher-title">
        <p>
          <b>{{ this.titleFormMode }}</b>
        </p>
      </div>
      <div class="form-detail-content">
        <div class="full-content">
          <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.UserName }}</label>
          <div class="container-input">
            <ms-input
              ref="user_name"
              v-model="user.user_name"
              :class="{ 'border-red': isBorderRed.user_name }"
              @input="setIsBorderRed('user_name')"
              @mouseenter="isHovering.user_name = true"
              @mouseleave="isHovering.user_name = false"
            ></ms-input>
            <div class="ms-tooltip" v-if="isHovering.user_name && isBorderRed.user_name">
              {{ errors["user_name"] }}
            </div>
          </div>
        </div>
        <div class="full-content">
          <div class="col-md-l" style="position: relative" ref="MenuItemRole">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.Role }}
              <span class="s-require">*</span>
            </label>
            <ms-combobox
              ref="ComboUser"
              :isBorderRedCBB="isBorderRed"
              :entityCBB="user"
              :errorsCBB="errors"
              :listEntitySearchCBB="listRoleSearch"
              :propName="'role_name'"
              :propId="'role_id'"
              :placeholderInputCBB="this.$_MSResource[this.$_LANG_CODE].FORM.PlaceholderRole"
            ></ms-combobox>
          </div>
        </div>
        <div class="full-content">
          <div class="col-md-l">
            <label>{{ $_MSResource[$_LANG_CODE].FORM.Status }}</label>
            <div class="e-gender">
              <input v-model="user.status" :value="1" type="radio" name="status" />
              <span>{{ $_MSResource[$_LANG_CODE].FORM.Active }}</span>
              <input v-model="user.status" :value="0" type="radio" name="status" />
              <span>{{ $_MSResource[$_LANG_CODE].FORM.InActive }}</span>
            </div>
          </div>
        </div>
      </div>
      <div class="form-detail-action">
        <div class="action-left">
          <ms-button-extra
            :textButtonExtra="this.$_MSResource[this.$_LANG_CODE].BUTTON.CANCEL"
            @click="btnCancel"
          ></ms-button-extra>
        </div>
        <div class="action-right">
          <ms-button-extra
            :textButtonExtra="this.$_MSResource[this.$_LANG_CODE].BUTTON.SAVE"
            @click="btnSave"
            :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.SAVE"
          ></ms-button-extra>
          <ms-button-default
            :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.SAVE_AND_ADD"
            @click="btnSaveAndAdd"
            :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.SAVE_AND_ADD"
          ></ms-button-default>
        </div>
      </div>
    </div>
    <!-- dialog teacher input data not blank -->
    <ms-dialog-data-not-null
      v-if="isShowDialogDataNotNull"
      :valueNotNull="dataNotNull"
      :title="this.$_MSResource[this.$_LANG_CODE].DIALOG.TITLE.DATA_INVALID"
    ></ms-dialog-data-not-null>
    <!-- dialog teacher save and close -->
    <ms-dialog-data-change v-if="isShowDialogDataChange"></ms-dialog-data-change>
  </div>
</template>

<script>
import roleService from "@/services/role.js";
import userService from "@/services/user.js";
import helperCommon from "@/helpers/helper.js";

export default {
  name: "UserDetail",

  props: ["userSelected", "statusFormMode"],

  created() {
    this.loadData();

    this.$_MSEmitter.on("cancelDialogDataChange", () => {
      this.onCancelDialogDataChange();
    });
    this.$_MSEmitter.on("noDialogDataChange", () => {
      this.onNoDialogDataChange();
    });
    this.$_MSEmitter.on("yesDialogDataChange", async () => {
      await this.onYesDialogDataChange();
    });
    this.$_MSEmitter.on("closeDialogDataError", () => {
      this.onCloseDialogSaveAndAdd();
    });

    this.$_MSEmitter.on("onSelectedEntityCBB", (data) => {
      this.onSelectedRole(data);
    });
    this.$_MSEmitter.on("onSearchChangeCBB", (newValue) => {
      this.onSearchChangeRole(newValue);
    });
    this.$_MSEmitter.on("onKeyDownEntityCBB", (index) => {
      this.user.role_name = this.listRoleSearch[index].role_name;
      this.user.role_id = this.listRoleSearch[index].role_id;
      this.isBorderRed.role_name = false;
    });
  },

  mounted() {
    // focus vào ô đầu tiên khi mở form chi tiết
    this.focusCode();
    // Đăng kí các sự kiện
    this.$refs.FormDetail.addEventListener("keydown", this.handleKeyDown);
  },

  data() {
    return {
      // Khai báo mảng lưu các thuộc tính cần validate theo thứ tự, phục vụ cho việc focus, hiển thị lỗi theo thứ tự
      userProperty: ["user_name", "role_id", "role_name"],
      // Khai báo đối tượng teacher
      user: {},
      // Khai báo danh sách đơn vị tìm kiếm
      listRoleSearch: [],
      // Khai báo trạng thái hiển thị của dialog cảnh báo dữ liệu k được để trống
      isShowDialogDataNotNull: false,
      // Khai báo biến xác định nội dung trường nào k được để trống
      dataNotNull: [],
      // Khai báo biến quy định trang thái hiển thị dialog dữ liệu đã bị thay đổi
      isShowDialogDataChange: false,
      // Khai báo biến xác định border red
      isBorderRed: {},
      // Khai báo biến quy định sau 1 khoảng thời gian mới thực hiện tìm kiếm ở combobox
      searchTimeout: null,
      // Khai báo biến lưu title form mode
      titleFormMode: this.$_MSResource[this.$_LANG_CODE].FORM.AddUser,
      // Khai báo biến chứa danh sách đối tượng lỗi
      errors: {},
      // Khai báo biến chứa danh sách các ô input khi hover
      isHovering: {},
    };
  },

  computed: {},

  methods: {
    /**
     * Mô tả: Hàm lấy danh sách subject từ api
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:10
     */
    async getListRole() {
      try {
        const res = await roleService.search("");
        this.listRoleSearch = res.data.Data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: gọi api lấy dữ liệu
     * created by : BNTIEN
     * created date: 30-05-2023 14:57:33
     */
    async loadData() {
      try {
        await this.getListRole();
        // Nếu form ở trạng thái thêm mới
        // Chuyển đối tượng sang chuỗi json
        let res = JSON.stringify(this.userSelected);
        // Chuyển đổi chuỗi json thành đối tượng teacher
        this.user = JSON.parse(res);
        if (this.statusFormMode !== this.$_MSEnum.FORM_MODE.Edit) {
          // Gán title cho form mode thêm mới
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.AddUser;
        } else {
          // Gán title cho form mode thêm sửa
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.UpdateUser;
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm focus vào ô input mã nhân viên
     * created by : BNTIEN
     * created date: 27-06-2023 01:53:48
     */
    focusCode() {
      let me = this;
      if (me.$refs.user_name) {
        me.$refs.user_name.focus();
      }
    },

    /**
     * Mô tả: Lắng nghe sự thay đổi text trong input search subject và tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChangeRole(newValue) {
      this.isBorderRed.role_name = false;
      this.isBorderRed.role_id = false;
      try {
        // Xóa bỏ timeout trước đó nếu có
        clearTimeout(this.searchTimeout);
        this.user.role_name = newValue;
        delete this.user.role_id;
        if (!newValue.trim()) {
          newValue = "";
        }
        this.searchTimeout = setTimeout(async () => {
          const newListRole = await roleService.search(newValue);
          this.listRoleSearch = newListRole.data.Data;
        }, 500);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm kiểm tra xem teacher có thay đổi sau khi mở form detail không
     * created by : BNTIEN
     * created date: 30-06-2023 00:21:22
     */
    hasDataChanged() {
      return JSON.stringify(this.userSelected) !== JSON.stringify(this.user);
    },
    /**
     * Mô tả: Hàm sử lí sự kiện khi click vào icon close
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:28
     */
    async onCloseFormDetail() {
      // Kiểm tra xem giữ liệu có thay đổi hay không (Trường hợp có thay đổi)
      if (this.hasDataChanged()) {
        this.isShowDialogDataChange = true;
      } else {
        this.$emit("closeFormDetail");
      }
    },

    /**
     * Mô tả: Hàm xử lí sự kiện khi người dùng chọn đơn vị
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedRole(role) {
      this.user.role_name = role.role_name;
      this.user.role_id = role.role_id;
      this.isBorderRed.role_name = false;
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
     * Mô tả: Hàm set các lỗi nhập liệu phía fontend
     * created by : BNTIEN
     * created date: 11-07-2023 16:36:05
     */
    setErrorMaxLength(key) {
      try {
        this.errors[key] = this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[key].Warning;
        this.isBorderRed[key] = true;
        this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[key].Warning);
      } catch {
        return;
      }
    },
    setErrorNotNumber(key) {
      try {
        this.errors[key] = this.$_MSResource[this.$_LANG_CODE].NOT_NUMBER[key];
        this.isBorderRed[key] = true;
        this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].NOT_NUMBER[key]);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Validate lỗi nhập liệu phía fontend
     * created by : BNTIEN
     * created date: 11-07-2023 10:07:22
     */
    validateScore() {
      try {
        for (const refInput of this.userProperty) {
          switch (refInput) {
            case "role_id":
              break;
            case "user_name":
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
     * Mô tả: Hàm xử lí lỗi nhập liệu người dùng khi backend trả về lỗi
     * created by : BNTIEN
     * created date: 29-06-2023 07:07:16
     */
    handleErrorInputUser(errors, userProperty) {
      const responseHandle = helperCommon.handleErrorInput(errors, userProperty);
      this.errors = responseHandle.error;
      this.isBorderRed = responseHandle.isBorderRed;
      this.dataNotNull = responseHandle.dataNotNull;
      if (this.dataNotNull.length > 0) {
        this.isShowDialogDataNotNull = true;
      }
    },

    /**
     * Mô tả: Hàm xử lí sự kiện khi người dùng bấm vào nút cất trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:05
     */
    async btnSave() {
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateScore();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            // set mật khẩu mặc định khi tạo tài khoản mới
            this.user.pass_word = "1";
            const res = await userService.create(this.user);
            if (
              res &&
              res.data &&
              this.$_MSEnum.CHECK_STATUS.isResponseStatusCreated(res.data.Code) &&
              res.data.Data > 0
            ) {
              this.$_MSEmitter.emit(
                "onShowToastMessage",
                this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_CTEATE
              );
              this.$emit("closeFormDetail");
              this.$_MSEmitter.emit("refreshDataTable");
            }
          } catch (error) {
            this.handleErrorInputUser(error, this.userProperty);
          }
        }
      } else {
        // Nếu form ở trạng thái sửa
        // Kiểm tra xem dữ liệu có thay đổi hay k (Trường hợp đã thay đổi)
        if (this.hasDataChanged()) {
          this.validateScore();
          if (this.dataNotNull.length > 0) {
            this.isShowDialogDataNotNull = true;
          } else {
            try {
              const res = await userService.update(this.user);
              if (
                res &&
                res.data &&
                this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code) &&
                res.data.Data > 0
              ) {
                this.$_MSEmitter.emit(
                  "onShowToastMessageUpdate",
                  this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_UPDATE
                );
                this.$emit("closeFormDetail");
                this.$_MSEmitter.emit("refreshDataTable");
              }
            } catch (error) {
              this.handleErrorInputUser(error, this.userProperty);
            }
          }
        } else {
          this.$emit("closeFormDetail");
        }
      }
      // }
    },
    /**
     * Mô tả: Hàm xử lí sự kiện khi người dùng bấm vào nut cất và thêm trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:23
     */
    async btnSaveAndAdd() {
      // Nếu form ở trạng thái thêm mới
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateScore();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            // Nếu mã nhân viên chưa tồn tại trong hệ thống
            const res = await userService.create(this.user);
            if (
              res &&
              res.data &&
              this.$_MSEnum.CHECK_STATUS.isResponseStatusCreated(res.data.Code) &&
              res.data.Data > 0
            ) {
              this.$_MSEmitter.emit(
                "onShowToastMessage",
                this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_CTEATE
              );
              this.teacher = {};
              this.isBorderRed = {};
              this.$_MSEmitter.emit("refreshDataTable");
              this.focusCode();
            }
          } catch (error) {
            this.handleErrorInputUser(error, this.userProperty);
          }
        }
        // Nếu form ở trạng thái sửa
      } else {
        // Kiểm tra xem dữ liệu có thay đổi hay k
        if (this.hasDataChanged()) {
          this.validateScore();
          if (this.dataNotNull.length > 0) {
            this.isShowDialogDataNotNull = true;
          } else {
            try {
              const res = await userService.update(this.user);
              this.teacher = {};
              this.$_MSEmitter.emit("setFormModeAdd");
              this.focusCode();
              this.$_MSEmitter.emit("refreshDataTable");
              if (
                res &&
                res.data &&
                this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code) &&
                res.data.Data > 0
              ) {
                this.$_MSEmitter.emit(
                  "onShowToastMessageUpdate",
                  this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_UPDATE
                );
              }
            } catch (error) {
              this.handleErrorInputUser(error, this.userProperty);
            }
          }
        } else {
          this.teacher = {};
          this.$_MSEmitter.emit("setFormModeAdd");
          this.focusCode();
        }
      }
    },
    /**
     * Mô tả: Hàm đóng dialog cảnh báo dữ liệu k được để trống và focus vào các ô trống
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:59
     */
    onCloseDialogSaveAndAdd() {
      this.isShowDialogDataNotNull = false;
      this.dataNotNull = [];
      let listPropError = [];
      for (const key in this.isBorderRed) {
        if (this.isBorderRed[key] === true) {
          listPropError.push(key);
        }
      }
      // thêm thuộc tính subject_name vào listPropError để xử lí focus nếu chưa có
      if (listPropError.includes("role_id") && !listPropError.includes("role_name")) {
        listPropError.push("role_name");
      }
      for (const prop of this.userProperty) {
        if (listPropError.includes(prop)) {
          // đợi DOM cập nhật trước khi thực thi focus
          if (prop === "role_id" || prop === "role_name") {
            this.$nextTick(() => {
              this.$refs.ComboUser.focusCombobox();
            });
          } else {
            this.$nextTick(() => {
              this.$refs[prop].focus();
            });
          }
          return;
        }
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
     * Mô tả: Hàm xử lí sự kiện khi click vào nút hủy trong form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:20
     */
    btnCancel() {
      this.$emit("closeFormDetail");
    },

    /**
     * Mô tả: Hàm xử lí sự kiện khi bấm vào button hủy trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:40:13
     */
    onCancelDialogDataChange() {
      this.isShowDialogDataChange = false;
      this.focusCode();
    },

    /**
     * Mô tả: Hàm xử lí sự kiện khi bấm vào button không trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:42:10
     */
    onNoDialogDataChange() {
      this.$emit("closeFormDetail");
    },

    /**
     * Mô tả: Hàm xử lí sự kiện khi bấm vào button có trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:43:38
     */
    async onYesDialogDataChange() {
      this.isShowDialogDataChange = false;
      await this.btnSave();
    },

    /**
     * Mô tả: Hàm reset tabindex về ô input mã nhân viên khi tab nhảy đến icon close
     * created by : BNTIEN
     * created date: 01-06-2023 14:24:19
     */
    resetTab() {
      this.focusCode();
    },

    /**
     * Mô tả: xử lí sự kiện khi bấm esc khi đang ở form detail
     * created by : BNTIEN
     * created date: 01-07-2023 01:05:25
     */
    async handleKeyDown(event) {
      if (event.key === "Escape") {
        // Nếu phím được nhấn là Esc, thực hiện hàm onCloseFormDetail
        await this.onCloseFormDetail();
      } else if (event.ctrlKey && event.key === "s") {
        event.preventDefault(); // Ngăn chặn hành động mặc định của trình duyệt khi nhấn phím Ctrl + S
        this.btnSave();
      } else if (event.ctrlKey && event.shiftKey && event.key === "S") {
        event.preventDefault(); // Ngăn chặn hành động mặc định của trình duyệt khi nhấn tổ hợp phím Ctrl + Shift + S
        this.btnSaveAndAdd();
      }
    },
  },

  beforeUnmount() {
    this.$_MSEmitter.off("cancelDialogDataChange");
    this.$_MSEmitter.off("noDialogDataChange");
    this.$_MSEmitter.off("yesDialogDataChange");
    this.$_MSEmitter.off("closeDialogDataError");
    this.$_MSEmitter.off("onSelectedEntityCBB");
    this.$_MSEmitter.off("onSearchChangeCBB");
    this.$_MSEmitter.off("onKeyDownEntityCBB");
    // Xóa các sự kiện đã đăng kí
    this.$refs.FormDetail.removeEventListener("keydown", this.handleKeyDown);
  },
};
</script>

<style scoped>
@import url(@/css/detailinfo.css);

.border-red {
  border: 1px solid red;
}
/* .dp__main, .dp__input_wrap, .dp__input{
      border-radius: 4px;
      height: 32px;
    } */
</style>
