<template>
  <div class="chart-wrapper">
    <div class="chart-content">
      <canvas id="render-chart"></canvas>
    </div>
  </div>
  <div class="chart-title">{{ this.$_MSResource[this.$_LANG_CODE].Chart.AvgScoreByClassRegistration }}</div>
</template>

<script>
import Chart from "chart.js/auto";
import studentService from "@/services/student.js";

export default {
  name: "AvgScoreByClassRegistration",
  data() {
    return {
      dataChart: [],
      chartInstance: null, // Thêm thuộc tính để lưu trữ tham chiếu đến biểu đồ
    };
  },

  async created() {
    await this.getDataChart();
  },

  methods: {
    async getDataChart() {
      let res = await studentService.getClassAverageScore();
      if (res && res.data && this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code) && res.data.Data) {
        this.dataChart = res.data.Data;
        this.renderChart(); // Gọi phương thức renderChart sau khi dữ liệu đã được lấy
      }
    },

    renderChart() {
      if (this.chartInstance) {
        this.chartInstance.destroy(); // Hủy biểu đồ cũ nếu đã tồn tại
      }

      const labels = this.dataChart.map((x) => x.class_name);
      const dataScore = this.dataChart.map((x) => x.average_score);

      this.chartInstance = new Chart(document.getElementById("render-chart"), {
        type: "bar",
        data: {
          labels: labels,
          datasets: [
            {
              label: "Điểm trung bình",
              data: dataScore,
              backgroundColor: "rgba(54, 162, 235, 0.5)",
            },
          ],
        },
        options: {
          maintainAspectRatio: false, // Cấu hình vô hiệu hóa tính năng tự động thay đổi kích thước
          scales: {
            y: {
              beginAtZero: true,
            },
          },
        },
      });

      // Cập nhật chiều rộng của canvas dựa trên số lượng lớp
      const chartContent = document.querySelector(".chart-content");
      const minWidth = labels.length * 60; // Đặt chiều rộng tối thiểu cho mỗi cột
      chartContent.style.minWidth = `${minWidth}px`;
    },
  },
};
</script>

<style>
.chart-wrapper {
  overflow-x: auto;
}

.chart-content {
  height: 470px;
  width: 100%; /* Đảm bảo chiều rộng đầy đủ */
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
