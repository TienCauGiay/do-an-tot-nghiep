import BaseServices from "./base";

class LocationService extends BaseServices {
    controller = "Locations";

    /**
     * Mô tả: Lọc vị trí địa lí theo vị trí địa lí cha
     * created by : BNTIEN
     * created date: 30-07-2023 13:05:39
     */
    async getFilter(grade, parentCode){
        const response = await this.entity.get(`${this.getBaseUrl()}/filter`, {
            params: {
                grade: grade,
                parentCode: parentCode
            }
        });
        return response;
    }
}

export default new LocationService();