<template>
  <div class="overlay-dialog">
    <div class="dialog-entity position-display-center" id="dialog-danger-entity">
      <div class="title-dialog">
        <h1>
          {{ this.title }}
        </h1>
        <div class="close-icon" @click="btnClose" :title="this.$_MSResource[this.$_LANG_CODE].BUTTON.CLOSE"></div>
      </div>
      <div class="dialog-content">
        <div class="danger-red-icon dialog-content-icon"></div>
        <div class="dialog-content-main">
          <li
            v-for="(item, index) in this.valueNotNull"
            :key="index"
            :class="{ 'list-item': this.valueNotNull.length < 2 }"
          >
            {{ item }}
          </li>
        </div>
      </div>
      <div class="dialog-danger-footer">
        <ms-button-default
          :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.CLOSE"
          @click="btnClose"
          ref="CloseForm"
          :tabindex="200"
          @keydown.tab.prevent="resetTab($event.target.value)"
        ></ms-button-default>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "DialogDataNotNull",
  props: ["valueNotNull", "title"],
  mounted() {
    this.resetTab();
  },
  methods: {
    resetTab() {
      this.$refs.CloseForm.$el.focus();
    },
    /**
     * Mô tả: Hàm đóng dialog
     * created by : BNTIEN
     * created date: 29-05-2023 08:35:28
     */
    btnClose() {
      this.$_MSEmitter.emit("closeDialogDataError");
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
