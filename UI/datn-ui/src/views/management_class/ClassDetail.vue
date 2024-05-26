<template>
  <div id="detail-info-user" class="position-display-center" ref="FormDetail">
    <div class="form-detail-toolbar">
      <!-- <div class="question-icon icon-tb" :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.HELP"></div> -->
      <div
        @click="onCloseFormDetail"
        class="close-icon icon-tb"
        id="teacher-exist"
        @keydown.tab.prevent="resetTab($event.target.value)"
        :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.EXIST"
      ></div>
    </div>
    <div class="form-detail-main">
      <div class="teacher-title">
        <p>
          <b>{{ this.titleFormMode }}</b>
        </p>
      </div>
      <div v-if="sessionPermission == $_MSEnum.PERMISSION.Admin" class="form-detail-content">
        <div class="full-content">
          <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.ClassCode }} <span class="s-require">*</span></label>
          <div class="container-input">
            <ms-input
              ref="classes_code"
              v-model="classes.classes_code"
              :class="{ 'border-red': isBorderRed.classes_code }"
              @input="setIsBorderRed('classes_code')"
              @mouseenter="isHovering.classes_code = true"
              @mouseleave="isHovering.classes_code = false"
              :disabled="statusFormMode === $_MSEnum.FORM_MODE.Edit"
              :maxLength="50"
            ></ms-input>
            <div class="ms-tooltip" v-if="isHovering.classes_code && isBorderRed.classes_code">
              {{ errors["classes_code"] }}
            </div>
          </div>
        </div>
        <div class="full-content">
          <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.ClassName }} <span class="s-require">*</span></label>
          <div class="container-input">
            <ms-input
              ref="classes_name"
              v-model="classes.classes_name"
              :class="{ 'border-red': isBorderRed.classes_name }"
              @input="setIsBorderRed('classes_name')"
              @mouseenter="isHovering.classes_name = true"
              @mouseleave="isHovering.classes_name = false"
              :maxLength="500"
            ></ms-input>
            <div class="ms-tooltip" v-if="isHovering.classes_name && isBorderRed.classes_name">
              {{ errors["classes_name"] }}
            </div>
          </div>
        </div>
        <div class="full-content">
          <div class="col-md-l" style="position: relative" ref="MenuItemFaculty">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.Faculty }}
              <span class="s-require">*</span>
            </label>
            <ms-combobox
              ref="ComboFaculty"
              :isBorderRedCBB="isBorderRed"
              :entityCBB="classes"
              :errorsCBB="errors"
              :listEntitySearchCBB="listFacultySearch"
              :propName="'faculty_name'"
              :propId="'faculty_id'"
              :placeholderInputCBB="this.$_MSResource[this.$_LANG_CODE].FORM.PlaceholderFaculty"
            ></ms-combobox>
          </div>
        </div>
      </div>
      <div class="form-detail-action">
        <div class="action-left">
          <ms-button-extra
            :textButtonExtra="this.$_MSResource[this.$_LANG_CODE].BUTTON.CANCEL"
            @click="btnCancel"
          ></ms-button-extra>
        </div>
        <div class="action-right" v-if="sessionPermission == $_MSEnum.PERMISSION.Admin">
          <ms-button-default
            :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.SAVE"
            @click="btnSave"
            :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.SAVE"
          ></ms-button-default>
        </div>
        <div class="action-right" v-else>
          <ms-button-default
            :textButtonDefault="this.$_MSResource[this.$_LANG_CODE].BUTTON.SAVE"
            @click="btnSave"
            :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.SAVE"
          ></ms-button-default>
        </div>
      </div>
    </div>
    <!-- dialog teacher input data not blank -->
    <ms-dialog-data-not-null
      v-if="isShowDialogDataNotNull"
      :valueNotNull="dataNotNull"
      :title="this.$_MSResource[this.$_LANG_CODE].DIALOG.TITLE.DATA_INVALID"
    ></ms-dialog-data-not-null>
    <!-- dialog student id Exist -->
    <ms-dialog-data-exist
      v-if="isShowDialogCodeExist"
      :textProp="this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_PRE"
      :textEntityCodeExist="contentCodeExist"
    ></ms-dialog-data-exist>
    <!-- dialog teacher save and close -->
    <ms-dialog-data-change v-if="isShowDialogDataChange"></ms-dialog-data-change>
  </div>
</template>

<script>
import classService from "@/services/classes.js";
import facultyService from "@/services/faculty.js";
import helperCommon from "@/helpers/helper.js";

export default {
  name: "ClassDetail",

  props: ["classSelected", "statusFormMode", "sessionPermission"],

  created() {
    this.loadData();

    this.$_MSEmitter.on("cancelDialogDataChange", () => {
      this.onCancelDialogDataChange();
    });
    this.$_MSEmitter.on("noDialogDataChange", () => {
      this.onNoDialogDataChange();
    });
    this.$_MSEmitter.on("yesDialogDataChange", async () => {
      await this.onYesDialogDataChange();
    });
    this.$_MSEmitter.on("closeDialogDataError", () => {
      this.onCloseDialogSaveAndAdd();
    });

    this.$_MSEmitter.on("onSelectedEntityCBB", (data) => {
      this.onSelectedFaculty(data);
    });
    this.$_MSEmitter.on("onSearchChangeCBB", (newValue) => {
      this.onSearchChange(newValue);
    });
    this.$_MSEmitter.on("onKeyDownEntityCBB", (index) => {
      this.classes.faculty_name = this.listFacultySearch[index].faculty_name;
      this.classes.faculty_id = this.listFacultySearch[index].faculty_id;
      this.isBorderRed.faculty_name = false;
    });

    this.$_MSEmitter.on("closeDialogCodeExist", () => {
      this.btnCloseDialogCodeExist();
    });
  },

  mounted() {
    // focus vào ô đầu tiên khi mở form chi tiết
    this.focusCode();
    // Đăng kí các sự kiện
    this.$refs.FormDetail.addEventListener("keydown", this.handleKeyDown);
  },

  data() {
    return {
      // Khai báo mảng lưu các thuộc tính cần validate theo thứ tự, phục vụ cho việc focus, hiển thị lỗi theo thứ tự
      classProperty: ["classes_code", "classes_name", "faculty_id", "faculty_name"],
      // Khai báo đối tượng teacher
      classes: {},
      // Khai báo trạng thái hiển thị của dialog cảnh báo dữ liệu k được để trống
      isShowDialogDataNotNull: false,
      // Khai báo biến xác định nội dung trường nào k được để trống
      dataNotNull: [],
      // Khai báo biến quy định trang thái hiển thị dialog dữ liệu đã bị thay đổi
      isShowDialogDataChange: false,
      // Khai báo biến xác định border red
      isBorderRed: {},
      // Khai báo biến quy định sau 1 khoảng thời gian mới thực hiện tìm kiếm ở combobox
      searchTimeout: null,
      // Khai báo biến lưu title form mode
      titleFormMode: this.$_MSResource[this.$_LANG_CODE].FORM.AddClass,
      // Khai báo biến chứa danh sách đối tượng lỗi
      errors: {},
      // Khai báo biến chứa danh sách các ô input khi hover
      isHovering: {},
      listFacultySearch: [],
      // Khai báo trạng thái hiển thị của dialog cảnh báo mã nhân viên đã tồn tại
      isShowDialogCodeExist: false,
      // Khai báo biến xác định thông tin của mã nhân viên đã tồn tại
      contentCodeExist: "",
    };
  },

  computed: {},

  methods: {
    /**
     * Mô tả: Hàm kiểm tra xem mã nhân viên đã tồn tại trong database hay chưa
     * created by : BNTIEN
     * created date: 29-06-2023 23:46:11
     */
    async checkExists() {
      try {
        const res = await classService.getByCode(this.classes.classes_code);
        return res.data;
      } catch {
        return null;
      }
    },
    /**
     * Mô tả: Hàm lấy danh sách faculty từ api
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:10
     */
    async getListFaculty() {
      try {
        const res = await facultyService.search("");
        this.listFacultySearch = res.data.Data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Lắng nghe sự thay đổi text trong input search faculty và tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChange(newValue) {
      this.isBorderRed.faculty_name = false;
      this.isBorderRed.faculty_id = false;
      try {
        // Xóa bỏ timeout trước đó nếu có
        clearTimeout(this.searchTimeout);
        this.classes.faculty_name = newValue;
        delete this.classes.faculty_id;
        if (!newValue.trim()) {
          newValue = "";
        }
        this.searchTimeout = setTimeout(async () => {
          const newListFaculty = await facultyService.search(newValue);
          this.listFacultySearch = newListFaculty.data.Data;
        }, 500);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng chọn đơn vị
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedFaculty(item) {
      this.classes.faculty_name = item.faculty_name;
      this.classes.faculty_id = item.faculty_id;
      this.isBorderRed.faculty_name = false;
    },
    /**
     * Mô tả: gọi api lấy dữ liệu
     * created by : BNTIEN
     * created date: 30-05-2023 14:57:33
     */
    async loadData() {
      try {
        await this.getListFaculty();
        // Nếu form ở trạng thái thêm mới
        // Chuyển đối tượng sang chuỗi json
        let res = JSON.stringify(this.classSelected);
        // Chuyển đổi chuỗi json thành đối tượng teacher
        this.classes = JSON.parse(res);
        if (this.statusFormMode !== this.$_MSEnum.FORM_MODE.Edit) {
          // Gán title cho form mode thêm mới
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.AddClass;
        } else {
          // Gán title cho form mode thêm sửa
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.UpdateClass;
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm focus vào ô input mã nhân viên
     * created by : BNTIEN
     * created date: 27-06-2023 01:53:48
     */
    focusCode() {
      let me = this;
      if (me.$refs.classes_code) {
        me.$refs.classes_code.focus();
      }
    },

    /**
     * Mô tả: Hàm kiểm tra xem teacher có thay đổi sau khi mở form detail không
     * created by : BNTIEN
     * created date: 30-06-2023 00:21:22
     */
    hasDataChanged() {
      return JSON.stringify(this.classSelected) !== JSON.stringify(this.classes);
    },
    /**
     * Mô tả: Hàm sử lý sự kiện khi click vào icon close
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:28
     */
    async onCloseFormDetail() {
      // Kiểm tra xem giữ liệu có thay đổi hay không (Trường hợp có thay đổi)
      if (this.hasDataChanged()) {
        this.isShowDialogDataChange = true;
      } else {
        this.$emit("closeFormDetail");
      }
    },

    /**
     * Mô tả: Hàm set các lỗi nhập liệu phía fontend
     * created by : BNTIEN
     * created date: 11-07-2023 16:36:05
     */
    setError(key) {
      try {
        this.errors[key] = this.$_MSResource[this.$_LANG_CODE].VALIDATE[key];
        this.isBorderRed[key] = true;
        this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].VALIDATE[key]);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm set các lỗi nhập liệu phía fontend
     * created by : BNTIEN
     * created date: 11-07-2023 16:36:05
     */
    setErrorMaxLength(key) {
      try {
        this.errors[key] = this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[key].Warning;
        this.isBorderRed[key] = true;
        this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[key].Warning);
      } catch {
        return;
      }
    },
    setErrorNotNumber(key) {
      try {
        this.errors[key] = this.$_MSResource[this.$_LANG_CODE].NOT_NUMBER[key];
        this.isBorderRed[key] = true;
        this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].NOT_NUMBER[key]);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Validate lỗi nhập liệu phía fontend
     * created by : BNTIEN
     * created date: 11-07-2023 10:07:22
     */
    validateClass() {
      try {
        for (const refInput of this.classProperty) {
          switch (refInput) {
            case "classes_code":
            case "classes_name":
              if (helperCommon.isEmptyInput(this.classes[refInput])) {
                this.setError(refInput);
              } else if (
                helperCommon.isMaxLengthInput(
                  this.classes[refInput],
                  this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[refInput].Limit
                )
              ) {
                this.setErrorMaxLength(refInput);
              }
              break;
            case "faculty_name":
              if (helperCommon.isEmptyInput(this.classes[refInput])) {
                this.setError(refInput);
              } else {
                if (!this.classes.faculty_id) {
                  this.errors.faculty_name = this.$_MSResource[this.$_LANG_CODE].VALIDATE.faculty_id;
                  this.isBorderRed.faculty_name = true;
                  this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].VALIDATE.faculty_id);
                }
              }
              break;
            default:
              break;
          }
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm xử lý lỗi nhập liệu người dùng khi backend trả về lỗi
     * created by : BNTIEN
     * created date: 29-06-2023 07:07:16
     */
    handleErrorInputClass(errors, classProperty) {
      const responseHandle = helperCommon.handleErrorInput(errors, classProperty);
      this.errors = responseHandle.error;
      this.isBorderRed = responseHandle.isBorderRed;
      this.dataNotNull = responseHandle.dataNotNull;
      if (this.dataNotNull.length > 0) {
        this.isShowDialogDataNotNull = true;
      }
    },

    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng bấm vào nút cất trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:05
     */
    async btnSave() {
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateClass();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            // Kiểm tra mã tồn tại
            let codeExist = (await this.checkExists()).Data;
            if (!codeExist) {
              const res = await classService.create(this.classes);
              if (
                res &&
                res.data &&
                this.$_MSEnum.CHECK_STATUS.isResponseStatusCreated(res.data.Code) &&
                res.data.Data > 0
              ) {
                this.$_MSEmitter.emit(
                  "onShowToastMessage",
                  this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_CTEATE
                );
                this.$emit("closeFormDetail");
                this.$_MSEmitter.emit("refreshDataTable");
              }
            } else {
              this.handleExisted(codeExist);
            }
          } catch (error) {
            this.handleErrorInputClass(error, this.classProperty);
          }
        }
      } else {
        // Nếu form ở trạng thái sửa
        // Kiểm tra xem dữ liệu có thay đổi hay k (Trường hợp đã thay đổi)
        if (this.hasDataChanged()) {
          this.validateClass();
          if (this.dataNotNull.length > 0) {
            this.isShowDialogDataNotNull = true;
          } else {
            try {
              let codeExist = (await this.checkExists()).Data;
              if (!codeExist || codeExist.classes_code === this.classSelected.classes_code) {
                const res = await classService.update(this.classes);
                if (
                  res &&
                  res.data &&
                  this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code) &&
                  res.data.Data > 0
                ) {
                  this.$_MSEmitter.emit(
                    "onShowToastMessageUpdate",
                    this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_UPDATE
                  );
                  this.$emit("closeFormDetail");
                  this.$_MSEmitter.emit("refreshDataTable");
                }
              } else {
                this.handleExisted(codeExist);
              }
            } catch (error) {
              this.handleErrorInputClass(error, this.classProperty);
            }
          }
        } else {
          this.$emit("closeFormDetail");
        }
      }
      // }
    },
    /**
     * Mô tả: Hàm xử lý khi mã nhân viên đã tồn tại trong hệ thống
     * created by : BNTIEN
     * created date: 30-06-2023 00:30:22
     */
    handleExisted(itemExist) {
      this.isShowDialogCodeExist = true;
      this.isBorderRed.classes_code = true;
      this.errors["classes_code"] = `${this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_PRE}
       ${itemExist.classes_code} ${this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_DETAIL_END}`;
      this.contentCodeExist = itemExist.classes_code;
    },
    /**
     * Mô tả: Hàm xử lý sự kiện đóng dialog cảnh báo mã nhân viên đã tồn tại
     * created by : BNTIEN
     * created date: 29-05-2023 08:28:19
     */
    btnCloseDialogCodeExist() {
      this.isShowDialogCodeExist = false;
      this.focusCode();
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng bấm vào nut cất và thêm trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:23
     */
    async btnSaveAndAdd() {
      // Nếu form ở trạng thái thêm mới
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateClass();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            // Nếu mã nhân viên chưa tồn tại trong hệ thống
            const res = await classService.create(this.classes);
            if (
              res &&
              res.data &&
              this.$_MSEnum.CHECK_STATUS.isResponseStatusCreated(res.data.Code) &&
              res.data.Data > 0
            ) {
              this.$_MSEmitter.emit(
                "onShowToastMessage",
                this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_CTEATE
              );
              this.teacher = {};
              this.isBorderRed = {};
              this.$_MSEmitter.emit("refreshDataTable");
              this.focusCode();
            }
          } catch (error) {
            this.handleErrorInputClass(error, this.classProperty);
          }
        }
        // Nếu form ở trạng thái sửa
      } else {
        // Kiểm tra xem dữ liệu có thay đổi hay k
        if (this.hasDataChanged()) {
          this.validateClass();
          if (this.dataNotNull.length > 0) {
            this.isShowDialogDataNotNull = true;
          } else {
            try {
              const res = await classService.update(this.classes);
              this.classes = {};
              this.$_MSEmitter.emit("setFormModeAdd");
              this.focusCode();
              this.$_MSEmitter.emit("refreshDataTable");
              if (
                res &&
                res.data &&
                this.$_MSEnum.CHECK_STATUS.isResponseStatusOk(res.data.Code) &&
                res.data.Data > 0
              ) {
                this.$_MSEmitter.emit(
                  "onShowToastMessageUpdate",
                  this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SUCCESS_UPDATE
                );
              }
            } catch (error) {
              this.handleErrorInputClass(error, this.classProperty);
            }
          }
        } else {
          this.teacher = {};
          this.$_MSEmitter.emit("setFormModeAdd");
          this.focusCode();
        }
      }
    },
    /**
     * Mô tả: Hàm đóng dialog cảnh báo dữ liệu k được để trống và focus vào các ô trống
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:59
     */
    onCloseDialogSaveAndAdd() {
      this.isShowDialogDataNotNull = false;
      this.dataNotNull = [];
      let listPropError = [];
      for (const key in this.isBorderRed) {
        if (this.isBorderRed[key] === true) {
          listPropError.push(key);
        }
      }
      // thêm thuộc tính faculty_name vào listPropError để xử lý focus nếu chưa có
      if (listPropError.includes("faculty_id") && !listPropError.includes("faculty_name")) {
        listPropError.push("faculty_name");
      }
      for (const prop of this.classProperty) {
        if (listPropError.includes(prop)) {
          // đợi DOM cập nhật trước khi thực thi focus

          // đợi DOM cập nhật trước khi thực thi focus
          if (prop === "faculty_id" || prop === "faculty_name") {
            this.$nextTick(() => {
              this.$_MSEmitter.emit("focusInputCBB");
            });
          } else {
            this.$nextTick(() => {
              this.$refs[prop].focus();
            });
            return;
          }
        }
      }
    },

    /**
     * Mô tả: Hàm bỏ border red khi dữ liệu thay đổi
     * created by : BNTIEN
     * created date: 29-06-2023 22:03:38
     */
    setIsBorderRed(prop) {
      if (prop in this.isBorderRed) {
        this.isBorderRed[prop] = false;
      }
    },

    /**
     * Mô tả: Hàm xử lý sự kiện khi click vào nút hủy trong form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:20
     */
    btnCancel() {
      this.$emit("closeFormDetail");
    },

    /**
     * Mô tả: Hàm xử lý sự kiện khi bấm vào button hủy trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:40:13
     */
    onCancelDialogDataChange() {
      this.isShowDialogDataChange = false;
      this.focusCode();
    },

    /**
     * Mô tả: Hàm xử lý sự kiện khi bấm vào button không trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:42:10
     */
    onNoDialogDataChange() {
      this.$emit("closeFormDetail");
    },

    /**
     * Mô tả: Hàm xử lý sự kiện khi bấm vào button có trong dialog dữ liệu đã bị thay đổi
     * created by : BNTIEN
     * created date: 30-05-2023 23:43:38
     */
    async onYesDialogDataChange() {
      this.isShowDialogDataChange = false;
      await this.btnSave();
    },

    /**
     * Mô tả: Hàm reset tabindex về ô input mã nhân viên khi tab nhảy đến icon close
     * created by : BNTIEN
     * created date: 01-06-2023 14:24:19
     */
    resetTab() {
      this.focusCode();
    },

    /**
     * Mô tả: xử lý sự kiện khi bấm esc khi đang ở form detail
     * created by : BNTIEN
     * created date: 01-07-2023 01:05:25
     */
    async handleKeyDown(event) {
      if (event.key === "Escape") {
        // Nếu phím được nhấn là Esc, thực hiện hàm onCloseFormDetail
        await this.onCloseFormDetail();
      } else if (event.ctrlKey && event.key === "s") {
        event.preventDefault(); // Ngăn chặn hành động mặc định của trình duyệt khi nhấn phím Ctrl + S
        this.btnSave();
      } else if (event.ctrlKey && event.shiftKey && event.key === "S") {
        event.preventDefault(); // Ngăn chặn hành động mặc định của trình duyệt khi nhấn tổ hợp phím Ctrl + Shift + S
        this.btnSaveAndAdd();
      }
    },
  },

  beforeUnmount() {
    this.$_MSEmitter.off("cancelDialogDataChange");
    this.$_MSEmitter.off("noDialogDataChange");
    this.$_MSEmitter.off("yesDialogDataChange");
    this.$_MSEmitter.off("closeDialogDataError");
    this.$_MSEmitter.off("onSelectedEntityCBB");
    this.$_MSEmitter.off("onSearchChangeCBB");
    this.$_MSEmitter.off("onKeyDownEntityCBB");
    this.$_MSEmitter.off("closeDialogCodeExist");
    // Xóa các sự kiện đã đăng kí
    this.$refs.FormDetail.removeEventListener("keydown", this.handleKeyDown);
  },
};
</script>

<style scoped>
@import url(@/css/detailinfo.css);

.border-red {
  border: 1px solid red;
}
/* .dp__main, .dp__input_wrap, .dp__input{
        border-radius: 4px;
        height: 32px;
      } */
</style>
