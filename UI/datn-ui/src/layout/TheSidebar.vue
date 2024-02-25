<template>
  <div id="sidebar" class="sidebar">
    <div class="sidebar-logo">
      <div class="icon-tb toggle toggle-icon"></div>
      <div class="logo-icon icon-l"></div>
      <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ACCOUNTANT }}</span>
    </div>
    <div class="menu">
      <ul class="nav">
        <li class="menu-item">
          <router-link to="/management-student" class="menu-item-a">
            <!-- <i class="icon-tb cash-icon"></i> -->
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR.ManagementStudent }}</span>
          </router-link>
        </li>
        <li class="menu-item">
          <router-link to="/management-teacher" class="menu-item-a">
            <!-- <i class="icon-tb cash-icon"></i> -->
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR.ManagementTeacher }}</span>
          </router-link>
        </li>
        <li class="menu-item">
          <router-link to="/management-score" class="menu-item-a">
            <!-- <i class="icon-tb cash-icon"></i> -->
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR.ManagementScore }}</span>
          </router-link>
        </li>
        <li class="menu-item">
          <router-link to="/statistic" class="menu-item-a">
            <!-- <i class="icon-tb cash-icon"></i> -->
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR.Statistic }}</span>
          </router-link>
        </li>
        <li class="menu-item">
          <router-link to="/management-account" class="menu-item-a">
            <!-- <i class="icon-tb cash-icon"></i> -->
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR.ManagementAccount }}</span>
          </router-link>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheSidebar",
};
</script>

<style>
@import url(@/css/sidebar.css);
</style>
