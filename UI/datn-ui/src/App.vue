<template>
  <div class="container">
    <!-- sidebar -->
    <SidebarLayout></SidebarLayout>
    <div id="content" class="content">
      <!-- header -->
      <HeaderLayout></HeaderLayout>
      <!-- content -->
      <ContentLayout></ContentLayout>
    </div>
  </div>
</template>

<script>
import HeaderLayout from "./layouts/HeaderLayout.vue";
import SidebarLayout from "./layouts/SidebarLayout.vue";
import ContentLayout from "./layouts/ContentLayout.vue";

export default {
  name: "App",

  components: {
    HeaderLayout,
    SidebarLayout,
    ContentLayout,
  },
};
</script>

<style scoped>
@import url(@/css/master.css);
</style>
