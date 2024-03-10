import BaseServices from "./base";
import MSResource from "@/scripts/resource.js";

class ScoreService extends BaseServices {
    controller = "Scores";  

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
     * Mô tả: Xử lí nhập khẩu điểm từ file excel
     * created by : BNTIEN
     * created date: 28-02-2024 22:01:49
     */
    async importExcel(formData){
        const response = await this.entity.post(`${this.getBaseUrl()}/import_excel`, formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        });
        return response;
    }

    /**
     * Mô tả: Xử lí xuất khẩu điểm ra file excel
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
}

export default new ScoreService();