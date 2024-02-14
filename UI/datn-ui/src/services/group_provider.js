import BaseServices from "./base";

class GroupProviderService extends BaseServices {
    controller = "GroupProviders";

    /**
     * Mô tả: Hàm lấy danh sách nhóm nhà cung cấp theo provider id
     * created by : BNTIEN
     * created date: 29-07-2023 17:10:24
     */
    async getByProviderId(providerId){
        const response = await this.entity.get(`${this.getBaseUrl()}/id/${providerId}`);
        return response;
    }
}

export default new GroupProviderService();