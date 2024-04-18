<template>
  <div id="sidebar" class="sidebar">
    <div class="sidebar-logo">
      <div class="icon-tb toggle toggle-icon"></div>
      <div class="logo-icon icon-l"></div>
      <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ACCOUNTANT }}</span>
    </div>
    <div class="menu">
      <ul class="nav">
        <template v-for="(item, index) in listSidebar" :key="index">
          <li class="menu-item">
            <router-link :to="item.Link" class="menu-item-a">
              <!-- <i class="icon-tb cash-icon"></i> -->
              <span>{{ item.Text }}</span>
            </router-link>
          </li>
        </template>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheSidebar",

  data() {
    return {
      permission: parseInt(sessionStorage.getItem("permission")),
      listSidebar: [],
    };
  },

  created() {
    let listAllSideBar = this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR;
    if (listAllSideBar) {
      this.listSidebar = listAllSideBar.filter((x) => x.Permission.includes(this.permission));
    }
  },
};
</script>

<style>
@import url(@/css/sidebar.css);
</style>
