import BaseServices from "./base";

class ScoreService extends BaseServices {
    controller = "Scores";  

    /**
     * Mô tả: Tìm kiếm phân trang
     * created by : BNTIEN
     * created date: 17-06-2023 03:50:28
     */ 
    async getFilter(limit, offset, textSearch, customFilter){
        const response = await this.entity.get(`${this.getBaseUrl()}/paging_filter`, {
            params: {
                limit: limit,
                offset: offset,
                textSearch: textSearch,
                customFilter: customFilter,
            }
        });
        return response;
    }

    async importExcel(formData){
        const response = await this.entity.post(`${this.getBaseUrl()}/import_excel`, formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        });
        return response;
    }

    
    /**
     * Mô tả: Xử lý xuất khẩu điểm ra file excel
     * created by : BNTIEN
     * created date: 04-07-2023 00:34:50
     */
    async exportData(link, limit, offset, textSearch, customFilter){
        const response = await this.entity.get(`${this.getBaseUrl()}/export`, { 
            responseType: 'blob', 
            params: {
                limit: limit,
                offset: offset,
                textSearch: textSearch,
                customFilter: customFilter,
            }   
        });
        const file = new Blob([response.data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
        const url = window.URL.createObjectURL(file);
        link.href = url;
        link.setAttribute('download', 'Danh_Sach_Diem');
        link.click();
        return response;
    }

    /**
     * Mô tả: Lấy dữ liệu cho combo chọn hình thức lọc
     * created by : BNTIEN
     * created date: 18-04-2024 21:40:19
     */
    async getOptionFilter(optionFilter, textSearch){
        const response = await this.entity.get(`${this.getBaseUrl()}/get_option_filter`, {
            params: {
                optionFilter: optionFilter, 
                textSearch: textSearch
            }
        });
        return response;
    }

    async checkExistsStudentInClassRegitration(obj){
        const response = await this.entity.post(`${this.getBaseUrl()}/check_exists_student_in_class_registration`, obj);
        return response;
    }
}

export default new ScoreService();