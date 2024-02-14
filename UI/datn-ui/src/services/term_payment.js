import BaseServices from "./base";

class TermPaymentService extends BaseServices {
    controller = "TermPayments";

    /**
     * Mô tả: Phân trang tìm kiếm điều khoản thanh toán
     * created by : BNTIEN
     * created date: 30-07-2023 13:05:39
     */
    async getFilter(pageSize, pageNumber, textSearch){
        const response = await this.entity.get(`${this.getBaseUrl()}/filter`, {
            params: {
                pageSize: pageSize,
                pageNumber: pageNumber,
                textSearch: textSearch
            }
        });
        return response;
    }
}

export default new TermPaymentService();