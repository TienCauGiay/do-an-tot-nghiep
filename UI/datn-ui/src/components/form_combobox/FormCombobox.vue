<template>
  <div
    class="form-cbb"
    :class="[
      {
        'border-red': isBorderRedFormCBB[propBorderRed],
      },
      { 'readonly-form-cbb': isReadonlyCBB },
    ]"
  >
    <div class="e-textfield-cbb">
      <div class="container-input">
        <ms-input
          ref="InputFormCBB"
          :placeholder="placeholderInputFormCBB"
          :value="valueInput"
          @input="onSearchChange($event)"
          :tabindex="tabindexFormCBB"
          @keydown="onKeyDownEntity"
          @mouseenter="isHoveringFormCBB = true"
          @mouseleave="isHoveringFormCBB = false"
          :class="{ 'readonly-input-cbb': isReadonlyCBB }"
          :readonly="isReadonlyCBB"
        ></ms-input>
        <div
          class="ms-tooltip"
          v-if="isHoveringFormCBB && isBorderRedFormCBB[propBorderRed] && errorsFormCBB[propBorderRed]"
        >
          {{ errorsFormCBB[propBorderRed] ? errorsFormCBB[propBorderRed] : errorsFormCBB[propBorderRed] }}
        </div>
      </div>
    </div>
    <div class="e-icon-cbb" @click="onShowSelectEntity" :class="{ 'display-none-cbb': isReadonlyCBB }">
      <div class="function-icon"></div>
    </div>
    <div class="form-cbb-menu" v-show="isShowSelectEntity" @click="onShowSelectEntity" ref="MenuCBB">
      <div class="form-cbb-menu-title">
        <div>{{ this.textColFirst }}</div>
        <div>
          {{ this.textColSecond }}
        </div>
      </div>
      <div class="content-form" ref="formCBB" @scroll="handleScroll">
        <template v-for="(item, index) in listEntitySearchFormCBB" :key="index">
          <div
            class="form-cbb-menu-item"
            @click="onSelectedEntity(item, index)"
            :class="[{ 'cbb-selected': index == indexEntitySelected }]"
            ref="EntitySelectedItem"
          >
            <div :class="`form-cbb-menu-item-${item.Grade}`">
              {{ item[propCode] }}
            </div>
            <div>{{ item[propName] }}</div>
          </div>
        </template>
        <div
          class="form-cbb-menu-item"
          v-if="!listEntitySearchFormCBB.length"
          :class="{ 'not-found-entity': !listEntitySearchFormCBB.length }"
        >
          {{ this.$_MSResource[this.$_LANG_CODE].FORM.NOT_FOUND }}
        </div>
      </div>
      <!-- <img class="form-cbb-loading" src="../../assets/img/loading.svg" alt="loading" v-if="isShowLoadding" /> -->
    </div>
  </div>
</template>

<script>
export default {
  name: "FormCombobox",

  props: [
    "propName",
    "propCode",
    "valueInput",
    "isBorderRedFormCBB",
    "entityFormCBB",
    "errorsFormCBB",
    "tabindexFormCBB",
    "listEntitySearchFormCBB",
    "placeholderInputFormCBB",
    "textColFirst",
    "textColSecond",
    "propBorderRed",
    "isReadonlyCBB",
    "indexSelectedCBB",
  ],

  data() {
    return {
      // Khai báo biến kiểm tra xem ô input có đang được hover hay không
      isHoveringFormCBB: false,
      // Khai báo biến quy định trạng thái hiển thị của combobox chọn đơn vị
      isShowSelectEntity: false,
      // Khai báo chỉ số đang được chọn trong combobox
      indexEntitySelected: 0,
    };
  },

  mounted() {
    window.addEventListener("mousedown", this.closeMenuCBB);
  },

  methods: {
    /**
     * Mô tả: Toggle menu
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:42
     */
    onShowSelectEntity() {
      if (!this.isReadonlyCBB) {
        this.isShowSelectEntity = !this.isShowSelectEntity;
      }
    },

    /**
     * Mô tả: Xử lý select item
     * created by : BNTIEN
     * created date: 29-05-2023 07:54:52`
     */
    onSelectedEntity(item, index) {
      this.$_MSEmitter.emit("onSelectedEntityFormCBB", item, this.propCode);
      this.indexEntitySelected = index;
    },

    /**
     * Mô tả: Tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChange($event) {
      try {
        this.isShowSelectEntity = true;
        await this.$_MSEmitter.emit("onSearchChangeFormCBB", $event.target.value, this.propCode);
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Hàm để xử lý khi scroll combobox
     * created by : BNTIEN
     * created date: 29-07-2023 22:45:32
     */
    async handleScroll() {
      try {
        const cbb = this.$refs.formCBB;
        const remainingSpace = cbb.scrollHeight - (cbb.scrollTop + cbb.clientHeight);

        // Kiểm tra xem đã scroll đến cuối combobox chưa
        if (remainingSpace <= 10) {
          // Gọi API để lấy thêm dữ liệu
          await this.$_MSEmitter.emit("handleScrollCBBformCBB", this.propCode, this.valueInput);
        }
      } catch {
        return;
      }
    },

    focus() {
      this.$refs["InputFormCBB"].focus();
    },

    /**
     * Mô tả: Hàm xử scroll theo khi bấm lên xuống
     * created by : BNTIEN
     * created date: 07-06-2023 08:37:33
     */
    scrollIndex(index, checkKeyCode) {
      try {
        const element = this.$refs.EntitySelectedItem[index];
        if (checkKeyCode === this.$_MSEnum.KEY_CODE.DOWN) {
          element.scrollIntoView({
            block: this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SCROLL.END,
          });
        } else if (checkKeyCode === this.$_MSEnum.KEY_CODE.UP) {
          element.scrollIntoView({
            block: this.$_MSResource[this.$_LANG_CODE].TEXT_CONTENT.SCROLL.START,
          });
        }
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Hàm xử lý sự kiện bấm lên xuống enter để chọn đơn vị
     * created by : BNTIEN
     * created date: 06-06-2023 22:02:18
     */
    onKeyDownEntity(event) {
      try {
        const maxLength = this.listEntitySearchFormCBB.length;
        if (maxLength == 0) {
          return;
        } else {
          if (event.keyCode == this.$_MSEnum.KEY_CODE.DOWN) {
            // Bấm xuống
            if (this.isShowSelectEntity) {
              if (this.indexEntitySelected < maxLength - 1) {
                this.indexEntitySelected++;
              } else if (this.indexEntitySelected == maxLength - 1) {
                this.indexEntitySelected = 0;
              }
              // scroll focus theo item được chọn
              this.scrollIndex(this.indexEntitySelected, this.$_MSEnum.KEY_CODE.DOWN);
            } else {
              this.isShowSelectEntity = true;
            }
          } else if (event.keyCode == this.$_MSEnum.KEY_CODE.UP) {
            // Bấm lên
            if (this.isShowSelectEntity) {
              if (this.indexEntitySelected > 0) {
                this.indexEntitySelected--;
              } else if (this.indexEntitySelected == 0) {
                this.indexEntitySelected = maxLength - 1;
              }
              // scroll focus theo item được chọn
              this.scrollIndex(this.indexEntitySelected, this.$_MSEnum.KEY_CODE.UP);
            } else {
              this.isShowSelectEntity = true;
            }
          } else if (event.keyCode == this.$_MSEnum.KEY_CODE.ENTER) {
            // Bấm enter
            if (this.isShowSelectEntity) {
              this.$_MSEmitter.emit("onKeyDownFormCBB", this.indexEntitySelected, this.propCode);
              this.isShowSelectEntity = false;
            } else {
              this.isShowSelectEntity = true;
            }
          }
        }
      } catch {
        return;
      }
    },

    /**
     * Mô tả: xử lý click outside
     * created by : BNTIEN
     * created date: 16-08-2023 09:48:35
     */
    closeMenuCBB() {
      if (this.isShowSelectEntity && !this.$refs.MenuCBB.contains(event.target)) {
        this.isShowSelectEntity = false;
      }
    },
  },

  beforeUnmount() {
    window.removeEventListener("mousedown", this.closeMenuCBB);
  },
};
</script>

<style scoped>
@import url(./FormCombobox.css);

.select-entity-block {
  background-color: #fff;
  border: 1px solid var(--color-border-default);
  z-index: 100;
}

.e-textfield-cbb .textfield {
  border-radius: 4px 0px 0px 4px;
}

.cbb-selected {
  background-color: var(--color-btn-default);
  color: #ffffff;
}

.not-found-entity {
  align-items: center;
  justify-content: center;
  text-align: center;
  opacity: 0.5;
}

.border-red {
  border: 1px solid red;
}

#info-payment-detail .border-red {
  border: 1px solid red;
}

#info-payment-detail .readonly-form-cbb {
  border: unset;
}

#info-payment-detail .readonly-form-cbb:has(.e-textfield-cbb input:hover),
#info-payment-detail .readonly-input-cbb:hover {
  background-color: #fff8cb;
  border: unset;
}

#info-payment-detail .readonly-form-cbb:has(.e-textfield-cbb input:focus),
#info-payment-detail .readonly-input-cbb:focus {
  border: unset;
  background-color: #f9ecca;
}

#info-payment-detail .display-none-cbb {
  display: none;
}
</style>
