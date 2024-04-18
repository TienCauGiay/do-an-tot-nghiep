<template>
  <div class="unauthorized-container">
    <div>
      <h1 class="unauthorized-container-title">403 - Không có quyền truy cập</h1>
      <p class="unauthorized-container-content">
        Bạn không có quyền truy cập vào trang này. Vui lòng liên hệ với Bùi Ngọc Tiến.
      </p>
    </div>
  </div>
</template>

<script>
export default {
  name: "NotPermission",
};
</script>

<style scoped>
@import url(./NotPermission.css);
</style>
