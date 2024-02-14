<template>
  <div class="overlay-dialog">
    <div class="dialog-entity position-display-center" id="dialog-warning-entity">
      <div class="title-dialog">
        <h1>
          {{ this.$_MSResource[this.$_LANG_CODE].DIALOG.TITLE.DATA_INVALID }}
        </h1>
        <div class="close-icon" @click="btnNo" :title="this.$_MSResource[this.$_LANG_CODE].BUTTON.CLOSE"></div>
      </div>
      <div class="dialog-content">
        <div class="warning-yellow-icon dialog-content-icon"></div>
        <div class="dialog-content-main">
          <p>
            {{ this.contentExist }}
          </p>
        </div>
      </div>
      <div class="dialog-warning-footer" id="dialog-warning-footer-handle">
        <ms-button-default
          :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.NO"
          @click="btnNo"
          ref="No"
          :tabindex="300"
        ></ms-button-default>
        <ms-button-default
          :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.YES"
          @click="btnYes"
          :tabindex="301"
          @keydown.tab.prevent="resetTab($event.target.value)"
        ></ms-button-default>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "DialogHandleExist",
  props: ["contentExist"],
  mounted() {
    this.resetTab();
  },
  methods: {
    resetTab() {
      this.$refs.No.$el.focus();
    },
    /**
     * Mô tả: Hàm gọi sự kiện đóng dialog dữ liệu đã tồn tại
     * created by : BNTIEN
     * created date: 29-05-2023 08:32:41
     */
    btnNo() {
      this.$_MSEmitter.emit("closeDialogHandleExist");
    },

    /**
     * Mô tả: Hàm gọi sự kiện xử lí khi chọn có
     * created by : BNTIEN
     * created date: 08-08-2023 08:09:46
     */
    btnYes() {
      this.$_MSEmitter.emit("confirmHandleExist");
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/dialog.css);

#dialog-warning-footer-handle {
  justify-content: space-between;
}
</style>
