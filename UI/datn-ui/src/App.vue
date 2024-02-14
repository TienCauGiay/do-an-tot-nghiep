<template>
  <div class="container">
    <!-- sidebar -->
    <TheSidebar></TheSidebar>
    <div id="content" class="content">
      <!-- header -->
      <TheHeader></TheHeader>
      <!-- main -->
      <TheMain></TheMain>
    </div>
  </div>
</template>

<script>
import TheSidebar from "./layout/TheSidebar.vue";
import TheHeader from "./layout/TheHeader.vue";
import TheMain from "./layout/TheMain.vue";

export default {
  name: "App",
  components: {
    TheSidebar,
    TheHeader,
    TheMain,
  },
};
</script>

<style>
@import url(@/css/blockexercise.css);
@import url(@/css/base/icon.css);
@import url(@/css/varialble.css);
</style>
