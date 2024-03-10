<template>
  <div class="content-padding">
    <div class="w-full">
      <div>
        <StatisticNumberStudent></StatisticNumberStudent>
      </div>
    </div>
  </div>
</template>

<script>
import StatisticNumberStudent from "./StatisticNumberStudent.vue";

export default {
  name: "StatisticPage",

  components: {
    StatisticNumberStudent,
  },

  data() {
    return {
      dataChart: [],
    };
  },
};
</script>

<style scoped>
.w-full {
  width: 100%;
}

.content-padding {
  padding: 10px;
}
</style>
