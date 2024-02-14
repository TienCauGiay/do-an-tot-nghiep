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
    async getByCode(code){
        const response = await this.entity.get(`${this.getBaseUrl()}/code/${code}`);
        return response;
    }

    /**
     * Mô tả: trả về bản trong database có id là tham số truyền vào
     * created by : BNTIEN
     * created date: 02-06-2023 22:09:41
     */
    async getById(id){
        const response = await this.entity.get(`${this.getBaseUrl()}/${id}`);
        return response;
    }

    /**
     * Mô tả: thêm mới 1 bản ghi vào database
     * created by : BNTIEN
     * created date: 02-06-2023 22:10:13
     */
    async create(obj){
        const response = await this.entity.post(this.getBaseUrl(), obj);
        return response;
    }

    /**
     * Mô tả: cập nhật thông tin 1 bản ghi có id là tham số truyền vào
     * created by : BNTIEN
     * created date: 02-06-2023 22:10:45
     */
    async update(id, obj){
        const response = await this.entity.put(`${this.getBaseUrl()}/${id}`, obj);
        return response;
    }
    
    /**
     * Mô tả: xóa 1 bản ghi theo id
     * created by : BNTIEN
     * created date: 02-06-2023 22:11:02
     */
    async delete(id){
        const response = await this.entity.delete(`${this.getBaseUrl()}/${id}`);
        return response;
    }

    /**
     * Mô tả: xóa nhiều bản ghi theo danh sách id
     * created by : BNTIEN
     * created date: 27-06-2023 23:05:28
     */
    async deleteMutiple(ids){
        const response = await this.entity.delete(`${this.getBaseUrl()}/ids`, {data:ids});
        return response;
    }

    /**
     * Mô tả: Tìm kiếm phân trang
     * created by : BNTIEN
     * created date: 17-06-2023 03:50:28
     */ 
    async getFilter(pageSize, pageNumber, textSearch){
        const response = await this.entity.get(`${this.getBaseUrl()}/filter`, {
            params: {
                pageSize: pageSize,
                pageNumber: pageNumber,
                textSearch: textSearch,
            }
        });
        return response;
    }
}

export default BaseServices;