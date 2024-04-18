import BaseServices from "./base";

class AuthService extends BaseServices{
    controller = "Auth" 

    /**
     * Mô tả: Đăng nhập ứng dụng
     * created by : BNTIEN
     * created date: 07-04-2024 15:11:27
     */
    async login(user){
        let payload = {
            user_name: btoa(user.user_name),
            pass_word: btoa(user.pass_word),
        }

        const response = await this.entity.post(`${this.getBaseUrl()}/authen`, payload);
        return response;
    }
}
export default new AuthService();