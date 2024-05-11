<template>
  <div class="content-header">
    <div class="header-title">
      <div id="gn" class="three-dashes-icon icon-tb"></div>
      <p :title="this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.NAME_COMPANY_SELECTED">
        {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.NAME_COMPANY_SELECTED }}
      </p>
      <div class="dropdown-grey-icon icon-tb"></div>
    </div>
    <div class="header-action">
      <!-- <div
        :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.SETTING"
        class="setting-icon icon-l"
        style="scale: 1.2 1.2"
      ></div>
      <div :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.HELP" class="question-icon icon-tb"></div>
      <div :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.ALERT" class="toast-notification-icon icon-l"></div> -->
      <div ref="Logout" class="info-user">
        <p>
          <!-- {{ this.userName }} -->
        </p>
        <div class="avatar-icon icon-l" @click="isShowLogout = !isShowLogout"></div>
        <div class="avartar-item" v-if="isShowLogout" @click="logout">
          {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.LOGOUT }}
        </div>
        <!-- <div class="dropdown-grey-icon icon-tb"></div> -->
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheHeader",

  data() {
    return {
      isShowLogout: false,
      userName: sessionStorage.getItem("user"),
    };
  },

  mounted() {
    window.addEventListener("click", this.handleClickOutsideLogout);
  },

  methods: {
    /**
     * Mô tả: Xử lý đăng xuất
     * created by : BNTIEN
     * created date: 18-04-2024 16:42:24
     */
    logout() {
      try {
        sessionStorage.removeItem("token");
        sessionStorage.removeItem("permission");
        sessionStorage.removeItem("user");
        this.$router.push("/login");
      } catch (error) {
        console.log(error);
      }
    },

    handleClickOutsideLogout(event) {
      if (this.$refs.Logout && !this.$refs.Logout.contains(event.target)) {
        this.isShowLogout = false;
      }
    },
  },

  beforeUnmount() {
    window.removeEventListener("click", this.handleClickOutsideLogout);
  },
};
</script>

<style scoped>
@import url(@/css/headercontent.css);

.avartar-item {
  position: absolute;
  width: 85px;
  top: 35px;
  left: -35px;
  border: 1px solid var(--color-border-default);
  border-radius: 4px;
  padding: 5px 10px;
  background: white;
  cursor: pointer;
}

.avartar-item:hover {
  background-color: var(--color-border-default);
  color: var(--color-btn-default);
}
</style>
