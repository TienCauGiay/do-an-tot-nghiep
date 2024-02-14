<template>
  <button class="btn btn-extra">
    <div class="text-extra">{{ textButtonExtra }}</div>
  </button>
</template>

<script>
export default {
  name: "ButtonExtra",
  props: ["textButtonExtra"],
};
</script>

<style scoped>
@import url(@/css/base/button.css);
</style>
