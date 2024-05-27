<template>
  <div id="detail-info" class="position-display-center" ref="FormDetail">
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
      <div v-if="sessionPermission == $_MSEnum.PERMISSION.Admin" id="form-detail-content" class="form-detail-content">
        <div class="half-content">
          <div class="col-md-l">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.RegistrationCode }} <span class="s-require">*</span></label
            >
            <div class="container-input">
              <ms-input
                ref="class_registration_code"
                v-model="registration.class_registration_code"
                :class="{ 'border-red': isBorderRed.class_registration_code }"
                @input="setIsBorderRed('class_registration_code')"
                @mouseenter="isHovering.class_registration_code = true"
                @mouseleave="isHovering.class_registration_code = false"
                :disabled="statusFormMode === $_MSEnum.FORM_MODE.Edit"
                :maxLength="50"
              ></ms-input>
              <div class="ms-tooltip" v-if="isHovering.class_registration_code && isBorderRed.class_registration_code">
                {{ errors["class_registration_code"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.RegistrationName }} <span class="s-require">*</span></label
            >
            <div class="container-input">
              <ms-input
                ref="class_registration_name"
                v-model="registration.class_registration_name"
                :class="{ 'border-red': isBorderRed.class_registration_name }"
                @input="setIsBorderRed('class_registration_name')"
                @mouseenter="isHovering.class_registration_name = true"
                @mouseleave="isHovering.class_registration_name = false"
                :disabled="statusFormMode === $_MSEnum.FORM_MODE.Edit"
                :maxLength="255"
              ></ms-input>
              <div class="ms-tooltip" v-if="isHovering.class_registration_name && isBorderRed.class_registration_name">
                {{ errors["class_registration_name"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l" style="position: relative" ref="MenuItemSubject">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.SubjectRegistration }}
              <span class="s-require">*</span>
            </label>
            <ms-combobox
              ref="ComboSubject"
              :isBorderRedCBB="isBorderRed"
              :entityCBB="registration"
              :errorsCBB="errors"
              :listEntitySearchCBB="listSubjectSearch"
              :propName="'subject_name'"
              :propId="'subject_id'"
              :placeholderInputCBB="this.$_MSResource[this.$_LANG_CODE].FORM.PlaceholderSubject"
              :disabled="statusFormMode === $_MSEnum.FORM_MODE.Edit"
            ></ms-combobox>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l" style="position: relative" ref="MenuItemSubject">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.Teacher }}
              <span class="s-require">*</span>
            </label>
            <ms-combobox
              ref="ComboTeacher"
              :isBorderRedCBB="isBorderRed"
              :entityCBB="registration"
              :errorsCBB="errors"
              :listEntitySearchCBB="listTeacherSearch"
              :propName="'teacher_name'"
              :propId="'teacher_id'"
              :placeholderInputCBB="this.$_MSResource[this.$_LANG_CODE].FORM.PlaceholderTeacher"
              :disabled="statusFormMode === $_MSEnum.FORM_MODE.Edit"
            ></ms-combobox>
          </div>
        </div>
        <div class="full-content-custom-2">
          <div class="col-md-l">
            <label>{{ this.$_MSResource[this.$_LANG_CODE].FORM.Student }} <span class="s-require">*</span></label>
            <ms-combobox-select-multiple
              ref="ComboMultiStudent"
              :listDataSelected="listStudentSelected"
              :propCode="'student_code'"
              :propId="'student_id'"
              :propName="'student_name'"
              :listEntitySearchCBB="listStudentSearch"
            ></ms-combobox-select-multiple>
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
import helperCommon from "@/helpers/helper.js";
import classRegistrationService from "@/services/class_registration.js";
import teacherService from "@/services/teacher.js";
import subjectService from "@/services/subject.js";
import studentService from "@/services/student.js";

export default {
  name: "RegistrationDetail",

  props: ["registrationSelected", "statusFormMode", "sessionPermission"],

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

    this.$_MSEmitter.on("onSelectedEntityCBB", (data, propName) => {
      if (propName == "subject_name") {
        this.onSelectedSubject(data);
      } else if (propName == "teacher_name") {
        this.onSelectedTeacher(data);
      }
    });
    this.$_MSEmitter.on("onSearchChangeCBB", (newValue, propName) => {
      if (propName == "subject_name") {
        this.onSearchChangeSubject(newValue);
      } else if (propName == "teacher_name") {
        this.onSearchChangeTeacher(newValue);
      }
    });
    this.$_MSEmitter.on("onKeyDownEntityCBB", (index, propName) => {
      if (propName == "subject_name") {
        this.registration.subject_name = this.listSubjectSearch[index].subject_name;
        this.registration.subject_id = this.listSubjectSearch[index].subject_id;
        this.isBorderRed.subject_name = false;
      } else if (propName == "teacher_name") {
        this.registration.teacher_name = this.listTeacherSearch[index].teacher_name;
        this.registration.teacher_id = this.listTeacherSearch[index].teacher_id;
        this.isBorderRed.teacher_name = false;
      }
    });

    this.$_MSEmitter.on("toggleCBBSelectMultiple", (item, isUnSelect) => {
      this.toggleSelectStudent(item, isUnSelect);
    });

    this.$_MSEmitter.on("deleteItemCBBSelectMultiple", (item) => {
      this.deleteStudent(item);
    });

    this.$_MSEmitter.on("handleScrollCBBSelectMultiple", async (textSearch) => {
      await this.handleScrollCBBStudent(textSearch);
    });

    this.$_MSEmitter.on("onSearchChangeCBBSelectMultiple", async (textSearch) => {
      await this.onSearchChangeStudent(textSearch);
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
      registrationProperty: [
        "class_registration_code",
        "class_registration_name",
        "subject_id",
        "subject_name",
        "teacher_id",
        "teacher_name",
      ],
      // Khai báo đối tượng teacher
      registration: {},
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
      titleFormMode: this.$_MSResource[this.$_LANG_CODE].FORM.AddRegistration,
      // Khai báo biến chứa danh sách đối tượng lỗi
      errors: {},
      // Khai báo biến chứa danh sách các ô input khi hover
      isHovering: {},
      listSubjectSearch: [],
      listTeacherSearch: [],
      listStudentSelected: [],
      listStudentSearch: [],
      currentPageStudent: 1,
      isValidateCustom: false,
      listRegistrationUpdate: [],
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
        const res = await classRegistrationService.getByCode(this.registration.class_registration_code);
        return res.data;
      } catch {
        return null;
      }
    },
    /**
     * Mô tả: Xử lí toggle select group
     * created by : BNTIEN
     * created date: 29-07-2023 21:47:44
     */
    toggleSelectStudent(item, isUnSelect) {
      try {
        if (isUnSelect) {
          this.listStudentSelected = this.listStudentSelected.filter((x) => x.student_id != item.student_id);
        } else {
          if (!this.listStudentSelected) {
            this.listStudentSelected = [];
          }
          this.listStudentSelected.push({
            student_id: item.student_id,
            student_code: item.student_code,
            student_name: item.student_name,
          });
        }
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Xóa 1 item trong cbb nhóm nhà cung cấp
     * created by : BNTIEN
     * created date: 29-07-2023 22:41:39
     */
    deleteStudent(item) {
      try {
        this.listStudentSelected = this.listStudentSelected.filter((x) => x.student_id != item.student_id);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Tìm kiếm, phân trang danh sách group
     * created by : BNTIEN
     * created date: 29-07-2023 20:57:23
     */
    async getListStudent() {
      try {
        const res = await studentService.getFilter(20, this.currentPageStudent, "", "");
        this.listStudentSearch = res.data.Data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: lấy thêm dữ liệu khi scroll đến cuối trong cbb nhóm nhà cung cấp
     * created by : BNTIEN
     * created date: 29-07-2023 22:53:56
     */
    async handleScrollCBBStudent(textSearch) {
      try {
        this.currentPageStudent += 1;
        const filtered = await studentService.getFilter(20, this.currentPageStudent, textSearch, "");
        this.listStudentSearch = [...this.listStudentSearch, ...filtered.data.Data];
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Tìm kiếm trong cbb nhóm nhà cung cấp
     * created by : BNTIEN
     * created date: 29-07-2023 23:23:56
     */
    async onSearchChangeStudent(textSearch) {
      try {
        const filtered = await studentService.getFilter(20, 1, textSearch, "");
        this.listStudentSearch = filtered.data.Data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm lấy danh sách subject từ api
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:10
     */
    async getListSubject() {
      try {
        const res = await subjectService.search("");
        this.listSubjectSearch = res.data.Data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng chọn đơn vị
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedSubject(item) {
      this.registration.subject_name = item.subject_name;
      this.registration.subject_id = item.subject_id;
      this.isBorderRed.subject_name = false;
    },
    /**
     * Mô tả: Lắng nghe sự thay đổi text trong input search subject và tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChangeSubject(newValue) {
      this.isBorderRed.subject_name = false;
      this.isBorderRed.subject_id = false;
      try {
        // Xóa bỏ timeout trước đó nếu có
        clearTimeout(this.searchTimeout);
        this.registration.subject_name = newValue;
        delete this.registration.subject_id;
        if (!newValue.trim()) {
          newValue = "";
        }
        this.searchTimeout = setTimeout(async () => {
          const newListSubject = await subjectService.search(newValue);
          this.listSubjectSearch = newListSubject.data.Data;
        }, 500);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm lấy danh sách subject từ api
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:10
     */
    async getListTeacher() {
      try {
        const res = await teacherService.search("");
        this.listTeacherSearch = res.data.Data;
      } catch {
        return;
      }
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng chọn đơn vị
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedTeacher(item) {
      this.registration.teacher_name = item.teacher_name;
      this.registration.teacher_id = item.teacher_id;
      this.isBorderRed.teacher_name = false;
    },
    /**
     * Mô tả: Lắng nghe sự thay đổi text trong input search subject và tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChangeTeacher(newValue) {
      this.isBorderRed.teacher_name = false;
      this.isBorderRed.teacher_id = false;
      try {
        // Xóa bỏ timeout trước đó nếu có
        clearTimeout(this.searchTimeout);
        this.registration.teacher_name = newValue;
        delete this.registration.teacher_id;
        if (!newValue.trim()) {
          newValue = "";
        }
        this.searchTimeout = setTimeout(async () => {
          const newListTeacher = await teacherService.search(newValue);
          this.listTeacherSearch = newListTeacher.data.Data;
        }, 500);
      } catch {
        return;
      }
    },
    /**
     * Mô tả: gọi api lấy dữ liệu
     * created by : BNTIEN
     * created date: 30-05-2023 14:57:33
     */
    async loadData() {
      try {
        // Nếu form ở trạng thái thêm mới
        // Chuyển đối tượng sang chuỗi json
        let res = JSON.stringify(this.registrationSelected);
        // Chuyển đổi chuỗi json thành đối tượng teacher
        this.registration = JSON.parse(res);
        if (this.statusFormMode !== this.$_MSEnum.FORM_MODE.Edit) {
          // Gán title cho form mode thêm mới
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.AddRegistration;
        } else {
          // Gán title cho form mode thêm sửa
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.UpdateRegistration;
          let listDetailUpdate = await classRegistrationService.getListDetail(this.registration.class_registration_id);
          if (listDetailUpdate && listDetailUpdate.data && listDetailUpdate.data.length > 0) {
            this.listStudentSelected = listDetailUpdate.data.map((x) => {
              let item = {
                student_id: x.student_id,
                student_code: x.student_code,
                student_name: x.student_name,
              };
              return item;
            });
          }
        }
        await this.getListTeacher();
        await this.getListSubject();
        await this.getListStudent();
      } catch {
        return;
      }
    },
    async getRegistrationUpdate() {
      try {
        let res = await classRegistrationService.GetMultipleByCode(this.registrationSelected.class_registration_code);
        return res.data;
      } catch {
        return [];
      }
    },
    /**
     * Mô tả: Hàm focus vào ô input mã nhân viên
     * created by : BNTIEN
     * created date: 27-06-2023 01:53:48
     */
    focusCode() {
      let me = this;
      if (me.$refs.class_registration_code) {
        me.$refs.class_registration_code.focus();
      }
    },

    /**
     * Mô tả: Hàm kiểm tra xem teacher có thay đổi sau khi mở form detail không
     * created by : BNTIEN
     * created date: 30-06-2023 00:21:22
     */
    hasDataChanged() {
      return false;
      // return JSON.stringify(this.registrationSelected) !== JSON.stringify(this.registration);
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
    validateSave() {
      try {
        for (const refInput of this.registrationProperty) {
          switch (refInput) {
            case "class_registration_code":
            case "class_registration_name":
              if (helperCommon.isEmptyInput(this.registration[refInput])) {
                this.setError(refInput);
              } else if (
                helperCommon.isMaxLengthInput(
                  this.registration[refInput],
                  this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[refInput].Limit
                )
              ) {
                this.setErrorMaxLength(refInput);
              }
              break;
            case "subject_name":
              if (helperCommon.isEmptyInput(this.registration[refInput])) {
                this.setError(refInput);
              } else {
                if (!this.registration.subject_id) {
                  this.errors.subject_name = this.$_MSResource[this.$_LANG_CODE].VALIDATE.subject_id;
                  this.isBorderRed.subject_name = true;
                  this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].VALIDATE.subject_id);
                }
              }
              break;
            case "teacher_name":
              if (helperCommon.isEmptyInput(this.registration[refInput])) {
                this.setError(refInput);
              } else {
                if (!this.registration.teacher_id) {
                  this.errors.teacher_name = this.$_MSResource[this.$_LANG_CODE].VALIDATE.teacher_id;
                  this.isBorderRed.teacher_name = true;
                  this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].VALIDATE.teacher_id);
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
    validateCustomSave() {
      if (!this.listStudentSelected || this.listStudentSelected.length == 0) {
        this.isValidateCustom = true;
      } else {
        this.isValidateCustom = false;
      }
    },
    /**
     * Mô tả: Hàm xử lý lỗi nhập liệu người dùng khi backend trả về lỗi
     * created by : BNTIEN
     * created date: 29-06-2023 07:07:16
     */
    handleErrorInputUser(errors, registrationProperty) {
      const responseHandle = helperCommon.handleErrorInput(errors, registrationProperty);
      this.errors = responseHandle.error;
      this.isBorderRed = responseHandle.isBorderRed;
      this.dataNotNull = responseHandle.dataNotNull;
      if (this.dataNotNull.length > 0) {
        this.isShowDialogDataNotNull = true;
      }
    },

    buildDataSave() {
      let itemSave = JSON.parse(JSON.stringify(this.registration));
      itemSave.ListDetail = this.listStudentSelected;
      return itemSave;
    },

    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng bấm vào nút cất trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:05
     */
    async btnSave() {
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateSave();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            // Kiểm tra mã tồn tại
            let codeExist = (await this.checkExists()).Data;
            if (!codeExist) {
              this.validateCustomSave();
              if (this.isValidateCustom) {
                this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].VALIDATE.StudentNotEmpty);
                this.isShowDialogDataNotNull = true;
              } else {
                let dataSave = this.buildDataSave();
                const res = await classRegistrationService.createMasterDetail(dataSave);
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
              }
            } else {
              this.handleExisted(codeExist);
            }
          } catch (error) {
            this.handleErrorInputUser(error, this.registrationProperty);
          }
        }
      } else {
        // Nếu form ở trạng thái sửa
        // Kiểm tra xem dữ liệu có thay đổi hay k (Trường hợp đã thay đổi)
        this.validateSave();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            let codeExist = (await this.checkExists()).Data;
            if (!codeExist || codeExist.class_registration_code === this.registrationSelected.class_registration_code) {
              this.validateCustomSave();
              if (this.isValidateCustom) {
                this.dataNotNull.push(this.$_MSResource[this.$_LANG_CODE].VALIDATE.StudentNotEmpty);
                this.isShowDialogDataNotNull = true;
              } else {
                let dataSave = this.buildDataSave();
                const res = await classRegistrationService.updateMasterDetail(dataSave);
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
              }
            } else {
              this.handleExisted(codeExist);
            }
          } catch (error) {
            this.handleErrorInputUser(error, this.registrationProperty);
          }
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
      this.isBorderRed.class_registration_code = true;
      this.errors["class_registration_code"] = `${this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_PRE}
       ${itemExist.class_registration_code} ${this.$_MSResource[this.$_LANG_CODE].DIALOG.CONTENT.EXIST_DETAIL_END}`;
      this.contentCodeExist = itemExist.class_registration_code;
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
     * Mô tả: Hàm đóng dialog cảnh báo dữ liệu k được để trống và focus vào các ô trống
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:59
     */
    onCloseDialogSaveAndAdd() {
      if (!this.isValidateCustom) {
        this.isShowDialogDataNotNull = false;
        this.dataNotNull = [];
        let listPropError = [];
        for (const key in this.isBorderRed) {
          if (this.isBorderRed[key] === true) {
            listPropError.push(key);
          }
        }
        // thêm thuộc tính subject_name vào listPropError để xử lý focus nếu chưa có
        if (listPropError.includes("subject_id") && !listPropError.includes("subject_name")) {
          listPropError.push("subject_name");
        }
        // thêm thuộc tính faculty_name vào listPropError để xử lý focus nếu chưa có
        if (listPropError.includes("teacher_id") && !listPropError.includes("teacher_name")) {
          listPropError.push("teacher_name");
        }
        for (const prop of this.registrationProperty) {
          if (listPropError.includes(prop)) {
            // đợi DOM cập nhật trước khi thực thi focus
            if (prop === "subject_id" || prop === "subject_name") {
              this.$nextTick(() => {
                if (this.$refs.ComboSubject) {
                  this.$refs.ComboSubject.focusCombobox();
                }
              });
            } else if (prop === "teacher_id" || prop === "teacher_name") {
              this.$nextTick(() => {
                if (this.$refs.ComboTeacher) {
                  this.$refs.ComboTeacher.focusCombobox();
                }
              });
            } else {
              this.$nextTick(() => {
                this.$refs[prop].focus();
              });
            }
            return;
          }
        }
      } else {
        if (this.$refs.ComboMultiStudent) {
          this.dataNotNull = [];
          this.isShowDialogDataNotNull = false;
          this.$refs.ComboMultiStudent.focusComboboxMulti();
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
    this.$_MSEmitter.off("toggleCBBSelectMultiple");
    this.$_MSEmitter.off("deleteItemCBBSelectMultiple");
    this.$_MSEmitter.off("handleScrollCBBSelectMultiple");
    this.$_MSEmitter.off("onSearchChangeCBBSelectMultiple");
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

.full-content-custom-2 {
  width: 100%;
  height: 220px;
  box-sizing: border-box;
}

#form-detail-content {
  padding-bottom: unset;
}
</style>
