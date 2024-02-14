<template>
  <div id="content-main" class="content-main">
    <router-view></router-view>
    <DialogSystemError></DialogSystemError>
  </div>
</template>

<script>
import DialogSystemError from "@/components/dialog/DialogSystemError.vue";
export default {
  name: "TheMain",

  components: {
    DialogSystemError,
  },
};
</script>

<style></style>
