import BaseServices from "./base";
import MSResource from "@/scripts/resource.js";

class EmployeeService extends BaseServices {
    controller = "Employees";

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
}

export default new EmployeeService();