import BaseServices from "./base";
import MSResource from "@/scripts/resource.js";

class StudentService extends BaseServices {
    controller = "Students"; 

    /**
     * Mô tả: Xuất danh sách nhân viên ra excel
     * created by : BNTIEN
     * created date: 04-07-2023 00:34:50
     */
    async exportData(link){
        const response = await this.entity.get(`${this.getBaseUrl()}/export`, { responseType: 'blob' });
        const file = new Blob([response.data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
        const url = window.URL.createObjectURL(file);
        link.href = url;
        link.setAttribute('download', MSResource["vn-VI"].TEXT_CONTENT.FILE_NAME);
        link.click();
        return response;
    }

    /**
     * Mô tả: Tìm kiếm phân trang
     * created by : BNTIEN
     * created date: 17-06-2023 03:50:28
     */ 
    async getFilter(limit, offset, textSearch){
        const response = await this.entity.get(`${this.getBaseUrl()}/paging_filter`, {
            params: {
                limit: limit,
                offset: offset,
                textSearch: textSearch,
            }
        });
        return response;
    }

    
    /**
     * Mô tả: Lấy dữ liệu cho Biểu đồ thống kê số sinh viên đầu vào/đầu ra Trường Đại học Kevin
     * created by : BNTIEN
     * created date: 10-03-2024 20:07:05
     */
    async getStatisticNumberStudent(){
        const response = await this.entity.get(`${this.getBaseUrl()}/get_statistic_number_student`);
        return response;
    }
}

export default new StudentService();