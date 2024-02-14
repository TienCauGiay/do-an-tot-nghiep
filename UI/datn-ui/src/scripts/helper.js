const helperCommon = {
  /**
   * Mô tả: Hàm xử lí chuỗi thành định dạng ngày tháng năm (dd/MM/yyyy)
   * created by : BNTIEN
   * created date: 04-06-2023 19:13:03
   */
    formatDate: (value) => {
        try {
          value = value.substring(0, 10);
          const dates = value.split("-");
          const res = `${dates[2]}/${dates[1]}/${dates[0]}`;
          return res;
        } catch (error) {
          return "";
        }
    },

    /**
     * Mô tả: Định dạng giá trị ngày giờ
     * created by : BNTIEN
     * created date: 08-08-2023 10:50:52
     */
    setNewDate: function(date) {
      const d = new Date(date);
      const year = d.getFullYear();
      const month = this.addLeadingZero(d.getMonth() + 1);
      const day = this.addLeadingZero(d.getDate());
      const hours = this.addLeadingZero(d.getHours());
      const minutes = this.addLeadingZero(d.getMinutes());
      const seconds = this.addLeadingZero(d.getSeconds());

      return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;
    },

    /**
     * Mô tả: Thêm số 0 nếu ngày < 10
     * created by : BNTIEN
     * created date: 08-08-2023 10:51:06
     */
    addLeadingZero: (value) =>{
      return value < 10 ? `0${value}` : value;
    },
    /**
     * Mô tả: Hàm xóa dấu tiếng việt
     * created by : BNTIEN
     * created date: 04-06-2023 19:13:39
     */
    removeVietnameseAccents: (str) => {
      return str
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, "")
        .replace(/đ/g, "d")
        .replace(/Đ/g, "D");
    },
    /**
     * Mô tả: Hàm kiểm tra ngày hợp lệ
     * created by : BNTIEN
     * created date: 08-06-2023 03:22:05
     */
    isInvalidDate: (value) => {
      const dob = new Date(value);      
      const dateToday = new Date();
      if(dob > dateToday){
        return true;
      }
      else{
        return false;
      }
    },
    /**
     * Mô tả: Hàm xử lí lỗi nhập liệu của thực thể khi input, update
     * created by : BNTIEN
     * created date: 04-07-2023 14:43:43
     */
    handleErrorInput: (errors, objectProperty) => {
      const dataNotNull = [];
      const isBorderRed = {};
      if(errors){
        for (const key of objectProperty) {
          if (key in errors) {
            dataNotNull.push(errors[key]);
            isBorderRed[key] = true;
          }
        }
      }
      return {
        'error': errors,
        'dataNotNull': dataNotNull,
        'isBorderRed': isBorderRed
      }
    },
    /**
     * Mô tả: Hàm kiểm tra dữ liệu rỗng
     * created by : BNTIEN
     * created date: 11-07-2023 13:29:51
     */
    isEmptyInput: (text) =>{
      if(!text){
        return true;
      }
      return false;
    },
    /**
     * Mô tả: Hàm kiểm tra dữ liệu vượt quá số kí tự cho phép
     * created by : BNTIEN
     * created date: 11-07-2023 14:01:11
     */
    isMaxLengthInput: (text, length) => {
      if(!text) return false;
      if(text.length > length){
        return true;
      }
      return false;
    },


    /**
     * Mô tả: Hàm kiểm tra dữ liệu tối thiểu là bao nhiêu
     * created by : BNTIEN
     * created date: 23-07-2023 12:10:12
     */
    isMinLengthInput: (text, length) => {
      if(!text) return false;
      if(text.length < length){
        return true;
      }
      return false;
    },

    /**
     * Mô tả: Hàm kiểm tra định dạng email 
     * created by : BNTIEN
     * created date: 11-07-2023 16:26:14
     */
    isFormatEmail: (text) => {
      if(!text) return false;
      const regex = /^\S+@\S+\.\S+$/;
      if(!regex.test(text)){
        return true;
      }
      return false;
    },

    /**
     * Mô tả: Hàm kiểm tra chuỗi có phải toàn số không
     * created by : BNTIEN
     * created date: 16-07-2023 14:45:44
     */
    isNumber: (text) => {
      if(!text) return false;
      const regex = /^[0-9]+$/;
      if(!regex.test(text)){
        return true;
      }
      return false;
    },

    /**
     * Mô tả: Hàm format tiền tệ
     * created by : BNTIEN
     * created date: 08-08-2023 10:51:26
     */
    formatCurrency: (value) =>{
      // Xử lý trường hợp số âm
      const isNegative = value < 0;
      const absoluteValue = Math.abs(value);

      // Định dạng số và thêm dấu cách phân cách phần nghìn và dấu chấm phân cách phần thập phân
      const formattedValue = absoluteValue.toLocaleString('de-DE', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
      });

      // Thêm dấu ngoặc đơn nếu số âm
      return isNegative ? `(${formattedValue})` : formattedValue;
    }
}

export default helperCommon;