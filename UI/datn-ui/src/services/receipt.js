import BaseServices from "./base";
import ReceiptResource from "@/scripts/resource_component/rc_receipt_payment.js";

class ReceiptService extends BaseServices {
    controller = "Receipts";

    /**
     * Mô tả: Lấy số chứng từ lớn nhất trong hệ thống
     * created by : BNTIEN
     * created date: 05-08-2023 00:33:45
     */
    async getCodeMax(){
        var response = await this.entity.get(`${this.getBaseUrl()}/maxcode`);
        return response;
    }

    /**
     * Mô tả: Tìm kiếm phân trang phiếu thu/chi
     * created by : BNTIEN
     * created date: 04-08-2023 05:39:31
     */
    async getFilter(pageSize, pageNumber, textSearch, keyFilter){
        const response = await this.entity.get(`${this.getBaseUrl()}/filter`, {
            params: {
                pageSize: pageSize,
                pageNumber: pageNumber,
                textSearch: textSearch,
                keyFilter: keyFilter
            }
        });
        return response;
    }

    /**
     * Mô tả: Hàm toggle 1 phiếu trạng thái ghi sổ/bỏ ghi
     * created by : BNTIEN
     * created date: 06-08-2023 15:26:00
     */
    async updateNote(obj){
        const response = await this.entity.put(`${this.getBaseUrl()}/note`, obj);
        return response;
    }

    /**
     * Mô tả: Hàm cập nhật trạng thái ghi sổ/bỏ ghi nhiều phiếu 
     * created by : BNTIEN
     * created date: 09-08-2023 05:39:05
     */
    async updateMultipleNote(ids, typeUpdate){
        const response = await this.entity.put(`${this.getBaseUrl()}/notes`, ids, { 
            typeUpdate: typeUpdate
        });
        return response;
    }

    /**
     * Mô tả: Xuất danh sách phiếu thu/chi ra excel
     * created by : BNTIEN
     * created date: 04-07-2023 00:34:50
     */
    async exportData(textSearch, keyFilter){
        const response = await this.entity.get(`${this.getBaseUrl()}/export`, { 
            params: {
                textSearch: textSearch,
                keyFilter: keyFilter
            }, 
            responseType: 'blob' 
        });
        const file = new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        const urlLink = window.URL.createObjectURL(file);
        const link = document.createElement('a');
        link.href = urlLink;
        link.setAttribute('download', ReceiptResource.FILE_NAME);
        link.click();
        return response;
    }
}

export default new ReceiptService();