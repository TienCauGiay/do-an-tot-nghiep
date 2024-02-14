<template>
  <div class="dialog-entity position-display-center">
    <div class="title-dialog">
      <h1>Xác nhận thay đổi</h1>
      <div class="close-icon" @click="btnClose" :title="this.$_MSResource[this.$_LANG_CODE].BUTTON.CLOSE"></div>
    </div>
    <div class="dialog-content">
      <div class="warning-yellow-icon dialog-content-icon"></div>
      <div class="dialog-content-main">
        <p>Bạn có muốn thiết lập trạng thái "Sử dụng" cho tất cả Tài khoản con không?</p>
      </div>
    </div>
    <div class="dialog-warning-delete-footer">
      <ms-button-extra
        @click="btnNoConfirm"
        :textButtonExtra="this.$_MSResource[this.$_LANG_CODE].BUTTON.NO"
        :tabindex="400"
        ref="NoConfirmDelete"
      ></ms-button-extra>
      <ms-button-default
        @click="btnConfirmDelete"
        :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.YES"
        :tabindex="401"
        @keydown.tab.prevent="resetTab($event.target.value)"
      ></ms-button-default>
    </div>
  </div>
</template>

<script>
export default {
  name: "DialogHandleTask",
  mounted() {
    this.resetTab();
  },
  methods: {
    resetTab() {
      this.$refs.NoConfirmDelete.$el.focus();
    },

    /**
     * Mô tả: Hàm gọi sự kiện xóa thực thể khi người dùng chọn có
     * created by : BNTIEN
     * created date: 29-05-2023 08:33:41
     */
    btnConfirmDelete() {
      this.$_MSEmitter.emit("confirmYesTaskEntity");
    },

    /**
     * Mô tả: Hàm gọi sự kiện hủy xóa thực thể khi người dùng chọn không
     * created by : BNTIEN
     * created date: 29-05-2023 08:35:00
     */
    btnNoConfirm() {
      this.$_MSEmitter.emit("confirmNoTaskEntity");
    },

    /**
     * Mô tả: Hàm đóng dialog
     * created by : BNTIEN
     * created date: 16-08-2023 22:20:42
     */
    btnClose() {
      this.$_MSEmitter.emit("closeTaskEntity");
    },
  },
};
</script>

<style>
@import url(@/css/base/dialog.css);

p {
  line-height: 1.5;
}
</style>
