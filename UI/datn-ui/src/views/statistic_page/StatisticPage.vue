<template>
  <div class="content-padding">
    <div class="w-400 select-chart">
      <ms-select-option
        :listData="listOptionFilter"
        :propCode="'option_code'"
        :propName="'option_name'"
        :entity="optionFilter"
        :placeholderValue="this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SelectChart"
        :indexSelect="listOptionFilter.findIndex((obj) => obj.option_code == optionFilter.option_code)"
        :isReadonly="true"
        :customStyle="'width: 400px;'"
      ></ms-select-option>
    </div>
    <div class="w-full">
      <div v-if="optionFilter && optionFilter.option_code === $_MSEnum.FILTER_CHART.StatisticNumberStudent">
        <StatisticNumberStudent></StatisticNumberStudent>
      </div>
      <div v-if="optionFilter && optionFilter.option_code === $_MSEnum.FILTER_CHART.AvgScoreByClassRegistration">
        <AvgScoreByClassRegistration></AvgScoreByClassRegistration>
      </div>
    </div>
  </div>
</template>

<script>
import StatisticNumberStudent from "./StatisticNumberStudent.vue";
import AvgScoreByClassRegistration from "./AvgScoreByClassRegistration.vue";

export default {
  name: "StatisticPage",
  components: {
    StatisticNumberStudent,
    AvgScoreByClassRegistration,
  },

  created() {
    this.$_MSEmitter.on("onSelectedSelectOption", async (item, propCode) => {
      if (propCode == "option_code") {
        await this.handleSelectOptionFilter(item);
      }
    });
  },

  data() {
    return {
      dataChart: [],
      optionFilter: {
        option_code: this.$_MSEnum.FILTER_CHART.StatisticNumberStudent,
        option_name: "Thống kê tỉ lệ đầu vào/đầu ra sinh viên",
      },
      listOptionFilter: [
        {
          option_code: this.$_MSEnum.FILTER_CHART.StatisticNumberStudent,
          option_name: "Thống kê tỉ lệ đầu vào/đầu ra sinh viên",
        },
        {
          option_code: this.$_MSEnum.FILTER_CHART.AvgScoreByClassRegistration,
          option_name: "Thống kê điểm trung bình sinh viên theo lớp",
        },
      ],
    };
  },

  methods: {
    /**
     * Mô tả: Xử lý chọn điều kiện lọc
     * created by : BNTIEN
     * created date: 18-04-2024 20:53:30
     */
    async handleSelectOptionFilter(item) {
      this.optionFilter = item;
    },
  },

  beforeUnmount() {
    this.$_MSEmitter.off("onSelectedSelectOption");
  },
};
</script>

<style scoped>
.w-full {
  width: 100%;
}

.w-400 {
  width: 400px;
}

.content-padding {
  padding: 10px;
}
</style>
