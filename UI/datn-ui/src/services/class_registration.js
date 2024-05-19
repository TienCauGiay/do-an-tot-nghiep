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

    /**
     * Mô tả: thêm mới 1 bản ghi vào database
     * created by : BNTIEN
     * created date: 02-06-2023 22:10:13
     */
    async createMasterDetail(payload){
        const response = await this.entity.post(`${this.getBaseUrl()}/insert_master_detail`, payload);
        return response;
    }

    /**
     * Mô tả: cập nhật thông tin 1 bản ghi có id là tham số truyền vào
     * created by : BNTIEN
     * created date: 02-06-2023 22:10:45
     */
    async updateMasterDetail(payload){
        const response = await this.entity.put(`${this.getBaseUrl()}/update_master_detail`, payload);
        return response;
    }

    /**
     * Mô tả: trả về bản trong database có id là tham số truyền vào
     * created by : BNTIEN
     * created date: 02-06-2023 22:09:41
     */
    async getListDetail(id){
        const response = await this.entity.get(`${this.getBaseUrl()}/get_list_detail?id=${id}`);
        return response;
    }
}

export default new ClassRegistrationService();