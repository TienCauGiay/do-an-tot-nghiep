import BaseServices from "./base";

class AccountantService extends BaseServices {
    controller = "Accountants";

    /**
     * Mô tả: Hàm lấy danh sách hạch toán theo receipt id
     * created by : BNTIEN
     * created date: 05-08-2023 20:50:40
     */
    async getByReceiptId(receiptId){
        const response = await this.entity.get(`${this.getBaseUrl()}/id/${receiptId}`);
        return response;
    }
}

export default new AccountantService();