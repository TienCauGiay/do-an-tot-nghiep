import BaseServices from "./base";
import MSResource from "@/scripts/resource.js";

class StudentService extends BaseServices {
    controller = "Students";

    /**
     * Mô tả: Lấy mã nhân viên lớn nhất trong hệ thống
     * created by : BNTIEN
     * created date: 17-06-2023 05:36:33
     */
    async getCodeMax(){
        var response = await this.entity.get(`${this.getBaseUrl()}/maxcode`);
        return response;
    }

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
}

export default new StudentService();