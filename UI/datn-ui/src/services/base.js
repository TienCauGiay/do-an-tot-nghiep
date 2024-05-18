import entity from "./entity";

/**
 * Mô tả: tạo class cha để thực hiện kế thừa khi call api
 * created by : BNTIEN
 * created date: 02-06-2023 22:07:04
 */
class BaseServices {
    entity = entity;
    controller = "";

    getBaseUrl(){
        return `/${this.controller}`;
    }

    /**
     * Mô tả: trả về toàn bộ dữ liệu của 1 table trong database
     * created by : BNTIEN
     * created date: 02-06-2023 22:07:48
     */
    async getAll(){
        const response = await this.entity.get(this.getBaseUrl());
        return response;
    }

    /**
     * Mô tả: trả về bản trong database có code là tham số truyền vào
     * created by : BNTIEN
     * created date: 02-06-2023 22:09:41
     */ 
    async getByCode(code) {
        const response = await this.entity.get(`${this.getBaseUrl()}/get_by_code?code=${code}`);
        return response;
    } 

    /**
     * Mô tả: trả về bản trong database có id là tham số truyền vào
     * created by : BNTIEN
     * created date: 02-06-2023 22:09:41
     */
    async getById(id){
        const response = await this.entity.get(`${this.getBaseUrl()}/get_by_id?id=${id}`);
        return response;
    }

    /**
     * Mô tả: thêm mới 1 bản ghi vào database
     * created by : BNTIEN
     * created date: 02-06-2023 22:10:13
     */
    async create(obj){
        const response = await this.entity.post(`${this.getBaseUrl()}/insert`, obj);
        return response;
    }

    /**
     * Mô tả: thêm mới 1 bản ghi vào database
     * created by : BNTIEN
     * created date: 02-06-2023 22:10:13
     */
    async createMultiple(payload){
        const response = await this.entity.post(`${this.getBaseUrl()}/insert_multiple`, payload);
        return response;
    }

    /**
     * Mô tả: cập nhật thông tin 1 bản ghi có id là tham số truyền vào
     * created by : BNTIEN
     * created date: 02-06-2023 22:10:45
     */
    async update(obj){
        const response = await this.entity.put(`${this.getBaseUrl()}/update`, obj);
        return response;
    }

    /**
     * Mô tả: cập nhật thông tin 1 bản ghi có id là tham số truyền vào
     * created by : BNTIEN
     * created date: 02-06-2023 22:10:45
     */
    async updateMultiple(payload){
        const response = await this.entity.put(`${this.getBaseUrl()}/update_multiple`, payload);
        return response;
    }
    
    /**
     * Mô tả: xóa 1 bản ghi theo id
     * created by : BNTIEN
     * created date: 02-06-2023 22:11:02
     */
    async delete(id) {
        const response = await this.entity.delete(`${this.getBaseUrl()}/delete?id=${id}`);
        return response;
    }     

    /**
     * Mô tả: xóa nhiều bản ghi theo danh sách id
     * created by : BNTIEN
     * created date: 27-06-2023 23:05:28
     */
    async deleteMutiple(ids){
        const response = await this.entity.delete(`${this.getBaseUrl()}/delete_multiple`, {data:ids});
        return response;
    }

    async search(textSearch){
        const response = await this.entity.get(`${this.getBaseUrl()}/search?textSearch=${textSearch}`);
        return response;
    }
}

export default BaseServices;