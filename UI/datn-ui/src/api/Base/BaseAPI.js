import instance from '../InstanceAPI'

/**
 * Mô tả: API cơ sở chứa các hàm dùng chung
 * created by : BNTIEN
 * created date: 14-01-2024 20:41:28
 */
class BaseAPI {
    instance = instance;
    controller = "";

    getBaseUrl(){
        return `/${this.controller}`;
    }

    /**
     * Mô tả: Lấy tất cả các bản ghi của một đối tượng
     * created by : BNTIEN
     * created date: 14-01-2024 20:42:34
     */
    async getAll(){
        const response = await this.instance.get(this.getBaseUrl());
        return response;
    }

    /**
     * Mô tả: Tìm kiếm bản ghi theo mã
     * created by : BNTIEN
     * created date: 14-01-2024 20:44:14
     */
    async getByCode(code){
        const response = await this.instance.get(`${this.getBaseUrl()}/code/${code}`);
        return response;
    }

    /**
     * Mô tả: Tìm kiếm bản ghi theo id
     * created by : BNTIEN
     * created date: 14-01-2024 20:45:30
     */
    async getById(id){
        const response = await this.instance.get(`${this.getBaseUrl()}/id/${id}`);
        return response;
    }

    /**
     * Mô tả: Thêm mới một bản ghi
     * created by : BNTIEN
     * created date: 14-01-2024 20:45:50
     */
    async create(obj){
        const response = await this.instance.post(this.getBaseUrl(), obj);
        return response;
    }

    /**
     * Mô tả: Cập nhật thông tin một bản ghi
     * created by : BNTIEN
     * created date: 14-01-2024 20:46:08
     */
    async update(obj){
        const response = await this.instance.put(this.getBaseUrl(), obj);
        return response;
    }
    
    /**
     * Mô tả: Xóa 1 bản ghi theo id
     * created by : BNTIEN
     * created date: 14-01-2024 20:47:19
     */
    async delete(id){
        const response = await this.instance.delete(`${this.getBaseUrl()}/${id}`);
        return response;
    }

    /**
     * Mô tả: Xóa nhiều bản ghi theo list id
     * created by : BNTIEN
     * created date: 14-01-2024 20:49:04
     */
    async deleteMutiple(ids){
        const response = await this.instance.delete(`${this.getBaseUrl()}/ids`, {data:ids});
        return response;
    }

    /**
     * Mô tả: Tìm kiếm phân trang
     * created by : BNTIEN
     * created date: 14-01-2024 20:49:33
     */
    async getFilter(pageSize, pageNumber, textSearch){
        const response = await this.instance.get(`${this.getBaseUrl()}/paging_filter`, {
            params: {
                pageSize: pageSize,
                pageNumber: pageNumber,
                textSearch: textSearch,
            }
        });
        return response;
    }
}

export default BaseAPI;