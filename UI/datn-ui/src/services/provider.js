import BaseServices from "./base";
import PROVIDER from "@/scripts/resource_component/rc_provider";

class ProviderService extends BaseServices {
    controller = "Providers";

    /**
     * Mô tả: Lấy mã nhà cung cấp lớn nhất trong hệ thống
     * created by : BNTIEN
     * created date: 28-07-2023 06:28:12
     */
    async getCodeMax(){
        var response = await this.entity.get(`${this.getBaseUrl()}/maxcode`);
        return response;
    }

    /**
     * Mô tả: Xuất excel
     * created by : BNTIEN
     * created date: 09-08-2023 14:41:33
     */
    async exportData(textSearch){
        const response = await this.entity.get(`${this.getBaseUrl()}/export`, { 
            params: {
                textSearch: textSearch,
            }, 
            responseType: 'blob' 
        });
        const file = new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        const urlLink = window.URL.createObjectURL(file);
        const link = document.createElement('a');
        link.href = urlLink;
        link.setAttribute('download', PROVIDER.FILE_NAME);
        link.click();
        return response;
    }
}

export default new ProviderService();