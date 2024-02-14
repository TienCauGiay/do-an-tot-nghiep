<template>
  <div class="content-header">
    <div class="header-title">
      <div id="gn" class="three-dashes-icon icon-tb"></div>
      <p :title="this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.NAME_COMPANY_SELECTED">
        {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.NAME_COMPANY_SELECTED }}
      </p>
      <div class="dropdown-grey-icon icon-tb"></div>
    </div>
    <div class="header-action">
      <div
        :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.SETTING"
        class="setting-icon icon-l"
        style="scale: 1.2 1.2"
      ></div>
      <div :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.HELP" class="question-icon icon-tb"></div>
      <div :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.ALERT" class="toast-notification-icon icon-l"></div>
      <div class="info-user">
        <div class="avatar-icon icon-l"></div>
        <p>
          {{ this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.NAME_ACCOUNT_LOGIN }}
        </p>
        <div class="dropdown-grey-icon icon-tb"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheHeader",
};
</script>

<style>
@import url(@/css/headercontent.css);
</style>
