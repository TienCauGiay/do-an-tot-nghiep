User
<template>
  <div class="chart-content">
    <canvas id="render-chart"></canvas>
  </div>
  <div class="chart-title">{{ this.$_MSResource[this.$_LANG_CODE].Chart.Employee.TitleChart }}</div>
</template>

<script>
import Chart from "chart.js/auto";
import studentService from "@/services/student.js";

export default {
  name: "StatisticNumberStudent",
  data() {
    return {
      dataChart: [],
    };
  },

  async created() {
    await this.getDataChart();
  },

  methods: {
    async getDataChart() {
      let res = await studentService.getStatisticNumberStudent();
      if (res && res.data && this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code) && res.data.Data) {
        this.dataChart = res.data.Data;
        this.renderChart(); // Gọi phương thức renderChart sau khi dữ liệu đã được lấy
      }
    },

    renderChart() {
      new Chart(document.getElementById("render-chart"), {
        type: "bar",
        data: {
          labels: this.dataChart.map((x) => x.label_year),
          datasets: [
            {
              label: "Số lượng đầu vào",
              data: this.dataChart.map((x) => x.admission_year_quantity),
              backgroundColor: "rgba(54, 162, 235, 0.5)",
            },
            {
              label: "Số lượng đầu ra",
              data: this.dataChart.map((x) => x.graduation_year_quantity),
              backgroundColor: "rgba(255, 99, 132, 0.5)",
            },
          ],
        },
        options: {
          maintainAspectRatio: false, // Cấu hình vô hiệu hóa tính năng tự động thay đổi kích thước
        },
      });
    },
  },
};
</script>

<style>
.chart-content {
  height: 500px;
}

.chart-title {
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 16px;
  font-weight: 700;
  padding-top: 20px;
}
</style>
