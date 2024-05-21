import BaseServices from "./base";

class UserService extends BaseServices {
    controller = "Users";  

    /**
     * Mô tả: Tìm theo username
     * created by : BNTIEN
     * created date: 17-06-2023 03:50:28
     */ 
    async getByUserName(username){
        const response = await this.entity.get(`${this.getBaseUrl()}/get_by_user_name`, {
            params: {
                username: username,
            }
        });
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

    /**
     * Mô tả: Lấy lại mật khẩu mặc định
     * created by : BNTIEN
     * created date: 13-03-2024 22:38:45
     */
    async resetPassWork(user_id) {
        const response = await this.entity.put(`${this.getBaseUrl()}/reset_pass_word`, null, {
            params: {
                user_id: user_id
            }
        });
        return response.data; 
    }
    
}

export default new UserService();