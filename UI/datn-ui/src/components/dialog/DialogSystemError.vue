<template>
  <div class="overlay-dialog" v-if="isShowDialogSystemError">
    <div class="dialog-entity position-display-center" id="dialog-danger-entity">
      <div class="title-dialog">
        <h1>
          {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.titleDialogSystemError }}
        </h1>
        <div class="close-icon" @click="btnClose" :title="this.$_MSResource[this.$_LANG_CODE].BUTTON.CLOSE"></div>
      </div>
      <div class="dialog-content">
        <div class="danger-red-icon dialog-content-icon"></div>
        <div class="dialog-content-main">
          <li class="list-item">{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.contentDialogSystemError }}</li>
        </div>
      </div>
      <div class="dialog-danger-footer">
        <ms-button-default
          :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.CLOSE"
          @click="btnClose"
          :tabindex="200"
        ></ms-button-default>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "DialogSystemError",

  data() {
    return {
      isShowDialogSystemError: false,
    };
  },

  methods: {
    /**
     * Mô tả: Hàm đóng dialog
     * created by : BNTIEN
     * created date: 29-05-2023 08:35:28
     */
    btnClose() {
      this.isShowDialogSystemError = false;
      localStorage.setItem("isShowDialogSystemError", "false");
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/dialog.css);

.dialog-content-main {
  max-height: 66px;
  overflow-y: auto;
}

.list-item {
  list-style: none;
}
</style>
