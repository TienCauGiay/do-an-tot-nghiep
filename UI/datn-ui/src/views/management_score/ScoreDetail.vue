<template>
  <div id="detail-info-score" class="position-display-center" ref="FormDetail">
    <div class="form-detail-toolbar">
      <div class="question-icon icon-tb" :title="this.$_MSResource[this.$_LANG_CODE].TOOLTIP.HELP"></div>
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
      <div class="form-detail-content">
        <div class="full-content">
          <div class="col-md-l" style="position: relative" ref="MenuItemClassRegistration">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.ClassRegistration }}
              <span class="s-require">*</span>
            </label>
            <ms-combobox
              ref="ComboClassRegistration"
              :isBorderRedCBB="isBorderRed"
              :entityCBB="score"
              :errorsCBB="errors"
              :listEntitySearchCBB="listClassRegistrationSearch"
              :propName="'class_registration_name'"
              :propId="'class_registration_id'"
              :placeholderInputCBB="this.$_MSResource[this.$_LANG_CODE].FORM.PlaceholderClassRegistration"
            ></ms-combobox>
          </div>
        </div>
        <div class="full-content">
          <div class="col-md-l" style="position: relative" ref="MenuItemStudent">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.Student }}
              <span class="s-require">*</span>
            </label>
            <ms-combobox
              ref="ComboStudent"
              :isBorderRedCBB="isBorderRed"
              :entityCBB="score"
              :errorsCBB="errors"
              :listEntitySearchCBB="listStudentSearch"
              :propName="'student_name'"
              :propId="'student_id'"
              :placeholderInputCBB="this.$_MSResource[this.$_LANG_CODE].FORM.PlaceholderStudent"
            ></ms-combobox>
          </div>
        </div>
        <div class="full-content">
          <div class="col-md-l" style="position: relative" ref="MenuItemTeacher">
            <label>
              {{ this.$_MSResource[this.$_LANG_CODE].FORM.Teacher }}
              <span class="s-require">*</span>
            </label>
            <ms-combobox
              ref="ComboTeacher"
              :isBorderRedCBB="isBorderRed"
              :entityCBB="score"
              :errorsCBB="errors"
              :listEntitySearchCBB="listTeacherSearch"
              :propName="'teacher_name'"
              :propId="'teacher_id'"
              :placeholderInputCBB="this.$_MSResource[this.$_LANG_CODE].FORM.PlaceholderTeacher"
            ></ms-combobox>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l">
            <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.ScoreAttendance }}</label>
            <div class="container-input">
              <ms-input
                ref="score_attendance"
                v-model="score.score_attendance"
                :class="{ 'border-red': isBorderRed.score_attendance }"
                @input="setIsBorderRed('score_attendance')"
                @mouseenter="isHovering.score_attendance = true"
                @mouseleave="isHovering.score_attendance = false"
              ></ms-input>
              <div class="ms-tooltip" v-if="isHovering.score_attendance && isBorderRed.score_attendance">
                {{ errors["score_attendance"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l">
            <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.ScoreTest }}</label>
            <div class="container-input">
              <ms-input
                ref="score_test"
                v-model="score.score_test"
                :class="{ 'border-red': isBorderRed.score_test }"
                @input="setIsBorderRed('score_test')"
                @mouseenter="isHovering.score_test = true"
                @mouseleave="isHovering.score_test = false"
              ></ms-input>
              <div class="ms-tooltip" v-if="isHovering.score_test && isBorderRed.score_test">
                {{ errors["score_test"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l">
            <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.ScoreExam }}</label>
            <div class="container-input">
              <ms-input
                ref="score_exam"
                v-model="score.score_exam"
                :class="{ 'border-red': isBorderRed.score_exam }"
                @input="setIsBorderRed('score_exam')"
                @mouseenter="isHovering.score_exam = true"
                @mouseleave="isHovering.score_exam = false"
              ></ms-input>
              <div class="ms-tooltip" v-if="isHovering.score_exam && isBorderRed.score_exam">
                {{ errors["score_exam"] }}
              </div>
            </div>
          </div>
        </div>
        <div class="half-content">
          <div class="col-md-l">
            <label> {{ this.$_MSResource[this.$_LANG_CODE].FORM.ScoreAverage }}</label>
            <div class="container-input">
              <ms-input
                ref="score_average"
                v-model="score.score_average"
                :class="{ 'border-red': isBorderRed.score_average }"
                @input="setIsBorderRed('score_average')"
                @mouseenter="isHovering.score_average = true"
                @mouseleave="isHovering.score_average = false"
              ></ms-input>
              <div class="ms-tooltip" v-if="isHovering.score_average && isBorderRed.score_average">
                {{ errors["score_average"] }}
              </div>
            </div>
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
        <div class="action-right">
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
    <!-- dialog teacher save and close -->
    <ms-dialog-data-change v-if="isShowDialogDataChange"></ms-dialog-data-change>
  </div>
</template>

<script>
import teacherService from "@/services/teacher.js";
import studentService from "@/services/student.js";
import classRegistrationService from "@/services/class_registration.js";
import scoreService from "@/services/score.js";
import helperCommon from "@/helpers/helper.js";

export default {
  name: "ScoreDetail",

  props: ["scoreSelected", "statusFormMode"],

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
      if (propName == "student_name") {
        this.onSelectedStudent(data);
      } else if (propName == "teacher_name") {
        this.onSelectedTeacher(data);
      } else if (propName == "class_registration_name") {
        this.onSelectedClassRegistration(data);
      }
    });
    this.$_MSEmitter.on("onSearchChangeCBB", (newValue, propName) => {
      if (propName == "student_name") {
        this.onSearchChangeStudent(newValue);
      } else if (propName == "teacher_name") {
        this.onSearchChangeTeacher(newValue);
      } else if (propName == "class_registration_name") {
        this.onSearchChangeClassRegistration(newValue);
      }
    });
    this.$_MSEmitter.on("onKeyDownEntityCBB", (index, propName) => {
      if (propName == "student_name") {
        this.teacher.student_name = this.listStudentSearch[index].student_name;
        this.teacher.student_id = this.listStudentSearch[index].student_id;
        this.isBorderRed.student_name = false;
      } else if (propName == "teacher_name") {
        this.teacher.teacher_name = this.listTeacherSearch[index].teacher_name;
        this.teacher.teacher_id = this.listTeacherSearch[index].teacher_id;
        this.isBorderRed.teacher_name = false;
      } else if (propName == "class_registration_name") {
        this.teacher.class_registration_name = this.listClassRegistrationSearch[index].class_registration_name;
        this.teacher.class_registration_id = this.listClassRegistrationSearch[index].class_registration_id;
        this.isBorderRed.class_registration_name = false;
      }
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
      scoreProperty: [
        "student_id",
        "student_name",
        "teacher_id",
        "teacher_name",
        "class_registration_id",
        "class_registration_name",
        "score_attendance",
        "score_test",
        "score_exam",
        "score_average",
      ],
      // Khai báo đối tượng teacher
      score: {},
      // Khai báo danh sách sinh viên tìm kiếm
      listStudentSearch: [],
      // Khai báo danh sách giảng viên tìm kiếm
      listTeacherSearch: [],
      // Khai báo danh sách lớp học phần tìm kiếm
      listClassRegistrationSearch: [],
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
      titleFormMode: this.$_MSResource[this.$_LANG_CODE].FORM.AddScore,
      // Khai báo biến chứa danh sách đối tượng lỗi
      errors: {},
      // Khai báo biến chứa danh sách các ô input khi hover
      isHovering: {},
    };
  },

  computed: {},

  methods: {
    /**
     * Mô tả: Hàm lấy danh sách subject từ api
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:10
     */
    async getListStudent() {
      try {
        const res = await studentService.search("");
        this.listStudentSearch = res.data.Data;
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
     * Mô tả: Hàm lấy danh sách subject từ api
     * created by : BNTIEN
     * created date: 29-05-2023 07:56:10
     */
    async getListClassRegistration() {
      try {
        const res = await classRegistrationService.search("");
        this.listClassRegistrationSearch = res.data.Data;
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
        await this.getListStudent();
        await this.getListTeacher();
        await this.getListClassRegistration();
        // Nếu form ở trạng thái thêm mới
        // Chuyển đối tượng sang chuỗi json
        let res = JSON.stringify(this.scoreSelected);
        // Chuyển đổi chuỗi json thành đối tượng teacher
        this.score = JSON.parse(res);
        if (this.statusFormMode !== this.$_MSEnum.FORM_MODE.Edit) {
          // Gán title cho form mode thêm mới
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.AddScore;
        } else {
          // Gán title cho form mode thêm sửa
          this.titleFormMode = this.$_MSResource[this.$_LANG_CODE].FORM.UpdateScore;
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
      if (me.$refs.ComboClassRegistration) {
        me.$refs.ComboClassRegistration.focusCombobox();
      }
    },

    /**
     * Mô tả: Lắng nghe sự thay đổi text trong input search subject và tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChangeStudent(newValue) {
      this.isBorderRed.student_name = false;
      this.isBorderRed.student_id = false;
      try {
        // Xóa bỏ timeout trước đó nếu có
        clearTimeout(this.searchTimeout);
        this.score.student_name = newValue;
        delete this.score.student_id;
        if (!newValue.trim()) {
          newValue = "";
        }
        this.searchTimeout = setTimeout(async () => {
          const newListStudent = await studentService.search(newValue);
          this.listStudentSearch = newListStudent.data.Data;
        }, 500);
      } catch {
        return;
      }
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
        this.score.teacher_name = newValue;
        delete this.score.teacher_id;
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
     * Mô tả: Lắng nghe sự thay đổi text trong input search subject và tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChangeClassRegistration(newValue) {
      this.isBorderRed.class_registration_name = false;
      this.isBorderRed.class_registration_id = false;
      try {
        // Xóa bỏ timeout trước đó nếu có
        clearTimeout(this.searchTimeout);
        this.score.class_registration_name = newValue;
        delete this.score.class_registration_id;
        if (!newValue.trim()) {
          newValue = "";
        }
        this.searchTimeout = setTimeout(async () => {
          const newListTeacher = await classRegistrationService.search(newValue);
          this.listClassRegistrationSearch = newListTeacher.data.Data;
        }, 500);
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Hàm kiểm tra xem teacher có thay đổi sau khi mở form detail không
     * created by : BNTIEN
     * created date: 30-06-2023 00:21:22
     */
    hasDataChanged() {
      return JSON.stringify(this.scoreSelected) !== JSON.stringify(this.score);
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
     * Mô tả: Hàm xử lý sự kiện khi người dùng chọn đơn vị
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedStudent(student) {
      this.score.student_name = student.student_name;
      this.score.student_id = student.student_id;
      this.isBorderRed.student_name = false;
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng chọn đơn vị
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedTeacher(teacher) {
      this.score.teacher_name = teacher.teacher_name;
      this.score.teacher_id = teacher.teacher_id;
      this.isBorderRed.teacher_name = false;
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng chọn đơn vị
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedClassRegistration(classRegistration) {
      this.score.class_registration_name = classRegistration.class_registration_name;
      this.score.class_registration_id = classRegistration.class_registration_id;
      this.isBorderRed.class_registration_name = false;
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
    validateScore() {
      try {
        for (const refInput of this.scoreProperty) {
          switch (refInput) {
            case "student_id":
            case "teacher_id":
            case "class_registration_id":
              break;
            case "student_name":
            case "teacher_name":
            case "class_registration_name":
              if (helperCommon.isEmptyInput(this.score[refInput])) {
                this.setError(refInput);
              }
              break;
            case "score_attendance":
            case "score_test":
            case "score_exam":
            case "score_average":
              if (this.score[refInput]) {
                if (
                  helperCommon.isMaxLengthInput(
                    this.score[refInput],
                    this.$_MSResource[this.$_LANG_CODE].MAXLENGTH[refInput].Limit
                  )
                ) {
                  this.setErrorMaxLength(refInput);
                } else if (!helperCommon.isDecimal(this.score[refInput].toString())) {
                  this.setErrorNotNumber(refInput);
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
    handleErrorInputTeacher(errors, scoreProperty) {
      const responseHandle = helperCommon.handleErrorInput(errors, scoreProperty);
      this.errors = responseHandle.error;
      this.isBorderRed = responseHandle.isBorderRed;
      this.dataNotNull = responseHandle.dataNotNull;
      if (this.dataNotNull.length > 0) {
        this.isShowDialogDataNotNull = true;
      }
    },

    convertScore() {
      let scoreCreate = JSON.parse(JSON.stringify(this.score));
      scoreCreate.score_attendance = parseFloat(this.score.score_attendance.toString());
      scoreCreate.score_test = parseFloat(this.score.score_test.toString());
      scoreCreate.score_exam = parseFloat(this.score.score_exam.toString());
      scoreCreate.score_average = parseFloat(this.score.score_average.toString());
      return scoreCreate;
    },

    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng bấm vào nút cất trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:05
     */
    async btnSave() {
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateScore();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            let scoreCreate = this.convertScore();
            const res = await scoreService.create(scoreCreate);
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
          } catch (error) {
            this.handleErrorInputTeacher(error, this.scoreProperty);
          }
        }
      } else {
        // Nếu form ở trạng thái sửa
        // Kiểm tra xem dữ liệu có thay đổi hay k (Trường hợp đã thay đổi)
        if (this.hasDataChanged()) {
          this.validateScore();
          if (this.dataNotNull.length > 0) {
            this.isShowDialogDataNotNull = true;
          } else {
            try {
              let scoreUpdate = this.convertScore();
              const res = await scoreService.update(scoreUpdate);
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
            } catch (error) {
              this.handleErrorInputTeacher(error, this.scoreProperty);
            }
          }
        } else {
          this.$emit("closeFormDetail");
        }
      }
      // }
    },
    /**
     * Mô tả: Hàm xử lý sự kiện khi người dùng bấm vào nut cất và thêm trên form chi tiết
     * created by : BNTIEN
     * created date: 29-05-2023 07:55:23
     */
    async btnSaveAndAdd() {
      // Nếu form ở trạng thái thêm mới
      if (this.statusFormMode === this.$_MSEnum.FORM_MODE.Add) {
        this.validateScore();
        if (this.dataNotNull.length > 0) {
          this.isShowDialogDataNotNull = true;
        } else {
          try {
            let scoreCreate = this.convertScore();
            // Nếu mã nhân viên chưa tồn tại trong hệ thống
            const res = await scoreService.create(scoreCreate);
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
            this.handleErrorInputTeacher(error, this.scoreProperty);
          }
        }
        // Nếu form ở trạng thái sửa
      } else {
        // Kiểm tra xem dữ liệu có thay đổi hay k
        if (this.hasDataChanged()) {
          this.validateScore();
          if (this.dataNotNull.length > 0) {
            this.isShowDialogDataNotNull = true;
          } else {
            try {
              let scoreUpdate = this.convertScore();
              const res = await scoreService.update(scoreUpdate);
              this.teacher = {};
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
              this.handleErrorInputTeacher(error, this.scoreProperty);
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
      // thêm thuộc tính subject_name vào listPropError để xử lý focus nếu chưa có
      if (listPropError.includes("student_id") && !listPropError.includes("student_name")) {
        listPropError.push("student_name");
      }
      if (listPropError.includes("teacher_id") && !listPropError.includes("teacher_name")) {
        listPropError.push("teacher_name");
      }
      if (listPropError.includes("class_registration_id") && !listPropError.includes("class_registration_name")) {
        listPropError.push("class_registration_name");
      }
      for (const prop of this.scoreProperty) {
        if (listPropError.includes(prop)) {
          // đợi DOM cập nhật trước khi thực thi focus
          if (prop === "student_id" || prop === "student_name") {
            this.$nextTick(() => {
              this.focusCode();
            });
          } else if (prop === "teacher_id" || prop === "teacher_name") {
            this.$nextTick(() => {
              if (this.$refs.ComboTeacher) {
                this.$refs.ComboTeacher.focusCombobox();
              }
            });
          } else if (prop === "class_registration_id" || prop === "class_registration_name") {
            this.$nextTick(() => {
              if (this.$refs.ComboClassRegistration) {
                this.$refs.ComboClassRegistration.focusCombobox();
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
