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
          <router-link to="/study-program" class="menu-item-a">
            <!-- <i class="icon-tb cash-icon"></i> -->
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR.StudyProgram }}</span>
          </router-link>
        </li>
        <li class="menu-item">
          <router-link to="/register-study" class="menu-item-a">
            <!-- <i class="icon-tb cash-icon"></i> -->
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR.RegisterStudy }}</span>
          </router-link>
        </li>
        <li class="menu-item">
          <router-link to="/exam-registration" class="menu-item-a">
            <!-- <i class="icon-tb cash-icon"></i> -->
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR.ExamRegistration }}</span>
          </router-link>
        </li>
        <li class="menu-item">
          <router-link to="/look-up-tuition-fees" class="menu-item-a">
            <!-- <i class="icon-tb cash-icon"></i> -->
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR.LookUpTuitionFees }}</span>
          </router-link>
        </li>
        <li class="menu-item">
          <router-link to="/look-up-point" class="menu-item-a">
            <!-- <i class="icon-tb cash-icon"></i> -->
            <span>{{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.ITEM_SIDEBAR.LookUpPoints }}</span>
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
