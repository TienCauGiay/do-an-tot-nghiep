import BaseServices from "./base";

class ClassRegistrationService extends BaseServices {
    controller = "ClassRegistrations"; 

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

    async GetMultipleByCode(code){
        const response = await this.entity.get(`${this.getBaseUrl()}/get_multiple_by_code`, {
            params: {
                code: code,
            }
        });
        return response;
    }
}

export default new ClassRegistrationService();