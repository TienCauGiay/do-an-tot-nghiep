<template>
  <div class="toast-success">
    <div class="toast-info">
      <div class="check-green-icon"></div>
      <p>
        <b>{{ this.$_MSResource[this.$_LANG_CODE].TOAST.SUCCESS }} </b>
        {{ this.contentToast }}
      </p>
    </div>
    <div class="toast-exits">
      <div class="icon-tb close-icon" @click="onCloseToastMessage"></div>
      <a href="#">{{ this.$_MSResource[this.$_LANG_CODE].TOAST.UNDO }}</a>
    </div>
  </div>
</template>

<script>
export default {
  name: "ToastSuccess",
  props: ["contentToast"],
  methods: {
    onCloseToastMessage() {
      this.$_MSEmitter.emit("closeToastMessage");
    },
  },
};
</script>

<style>
@import url(@/css/base/toastmessage.css);
</style>
