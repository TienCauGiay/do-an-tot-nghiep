<template>
  <div class="overlay-dialog">
    <div class="dialog-entity position-display-center" id="dialog-warning-entity">
      <div class="title-dialog">
        <h1>
          {{ this.$_MSResource[this.$_LANG_CODE].DIALOG.TITLE.DATA_INVALID }}
        </h1>
        <div class="close-icon" @click="btnAgree" :title="this.$_MSResource[this.$_LANG_CODE].BUTTON.CLOSE"></div>
      </div>
      <div class="dialog-content">
        <div class="warning-yellow-icon dialog-content-icon"></div>
        <div class="dialog-content-main">
          <p>
            {{ this.textProp }}
            &lt;{{ this.textEntityCodeExist }}&gt;
            {{ this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_END }}
          </p>
        </div>
      </div>
      <div class="dialog-warning-footer">
        <ms-button-default
          :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.AGREE"
          @click="btnAgree"
          ref="Agree"
          :tabindex="300"
          @keydown.tab.prevent="resetTab($event.target.value)"
        ></ms-button-default>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "DialogDataExist",
  props: ["textProp", "textEntityCodeExist"],
  mounted() {
    this.resetTab();
  },
  methods: {
    resetTab() {
      this.$refs.Agree.$el.focus();
    },
    /**
     * Mô tả: Hàm gọi sự kiện đóng dialog dữ liệu đã tồn tại
     * created by : BNTIEN
     * created date: 29-05-2023 08:32:41
     */
    btnAgree() {
      this.$_MSEmitter.emit("closeDialogCodeExist");
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/dialog.css);

p {
  line-height: 1.5;
}
</style>
