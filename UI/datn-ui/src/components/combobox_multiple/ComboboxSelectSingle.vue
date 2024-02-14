<template>
  <div
    class="cbb-multiple"
    id="cbb-multiple"
    :class="{
      'border-red': isShowCode
        ? isBorderRedCBB[propId] || isBorderRedCBB[propCode]
        : isBorderRedCBB[propId] || isBorderRedCBB[propName],
    }"
  >
    <div class="input-cbb-mutiple">
      <div class="container-input">
        <ms-input
          ref="InputCBB"
          :value="isShowCode ? entityCBB[propCode] : entityCBB[propName]"
          @input="onSearchChange($event)"
          @keydown="onKeyDownEntity"
          @mouseenter="isHoveringCBB = true"
          @mouseleave="isHoveringCBB = false"
          :class="{ 'readonly-input-cbb': isReadonlyCBB }"
          :readonly="isReadonlyCBB"
        ></ms-input>
        <div class="ms-tooltip" v-if="isShowTooltip">
          {{ contentTooltip }}
        </div>
      </div>
    </div>
    <div class="icon-cbb-multiple" :class="{ 'readonly-input-cbb': isReadonlyCBB }">
      <div class="icon-l plus-green-icon"></div>
    </div>
    <div class="e-icon-cbb" @click="openMenu" :class="{ 'readonly-input-cbb': isReadonlyCBB }">
      <div class="function-icon"></div>
    </div>
  </div>
  <div
    class="select-item-cbb-multiple"
    style="position: relative"
    v-if="isShowSelectEntity"
    @click="this.isShowSelectEntity = !this.isShowSelectEntity"
    ref="MenuCBB"
  >
    <div class="form-cbb-menu" id="form-cbb-menu-select-single">
      <div class="form-cbb-menu-title">
        <div v-for="(item, index) in haederCBB" :key="index" :class="`col-${index + 1}-cbb-menu-item`">
          {{ item }}
        </div>
      </div>
      <div class="form-cbb-main" ref="cbbSelectSingle" @scroll="handleScroll">
        <template v-for="(item, index) in listEntitySearchCBB.Data" :key="item[propId]">
          <div
            class="form-cbb-menu-item"
            @click="onSelectedEntity(item, index)"
            :class="{ 'cbb-selected': index == indexEntitySelected }"
            ref="EntitySelectedItem"
          >
            <template v-for="(col, index1) in bodyCBB" :key="index1">
              <div :class="`col-${index1 + 1}-cbb-menu-item`">
                {{ item[col] }}
              </div>
            </template>
          </div>
        </template>
        <div
          class="form-cbb-menu-item"
          v-if="!listEntitySearchCBB.Data.length"
          :class="{
            'not-found-entity': !listEntitySearchCBB.Data.length,
          }"
        >
          Không tìm thấy
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ComboboxMultiple",

  props: [
    "propId",
    "propName",
    "propCode",
    "haederCBB",
    "bodyCBB",
    "isBorderRedCBB",
    "entityCBB",
    "errorsCBB",
    "listEntitySearchCBB",
    "isShowCode",
    "indexSelectedCBB",
    "isReadonlyCBB",
    "isFocusCBB",
  ],

  computed: {
    isShowTooltip() {
      if (this.isShowCode) {
        return (
          this.isHoveringCBB &&
          (this.isBorderRedCBB[this.propCode] || this.isBorderRedCBB[this.propId] || !this.entityCBB[this.propCode]) &&
          (this.errorsCBB[this.propCode] || this.errorsCBB[this.propId])
        );
      } else {
        return (
          this.isHoveringCBB &&
          (this.isBorderRedCBB[this.propName] || this.isBorderRedCBB[this.propId] || !this.entityCBB[this.propName]) &&
          (this.errorsCBB[this.propName] || this.errorsCBB[this.propId])
        );
      }
    },

    contentTooltip() {
      if (this.isShowCode) {
        return this.errorsCBB[this.propCode] ? this.errorsCBB[this.propCode] : this.errorsCBB[this.propId];
      } else {
        return this.errorsCBB[this.propName] ? this.errorsCBB[this.propName] : this.errorsCBB[this.propId];
      }
    },
  },

  data() {
    return {
      // Biến quy định trạng thái hiển thị select menu
      isShowSelectEntity: false,
      // Khai báo biến kiểm tra xem ô input có đang được hover hay không
      isHoveringCBB: false,
      // Khai báo chỉ số đang được chọn trong combobox
      indexEntitySelected: 0,
      // Biến quy định trạng thái focus input
      isFocus: false,
    };
  },

  created() {
    this.isFocus = this.isFocusCBB;
  },

  mounted() {
    if (this.indexSelectedCBB) {
      this.indexEntitySelected = this.indexSelectedCBB;
    }
    if (this.isFocus) {
      this.$refs["InputCBB"].focus();
      this.isFocus = false;
    }
    window.addEventListener("mousedown", this.closeMenuCBB);
  },

  methods: {
    /**
     * Mô tả: Toggle menu
     * created by : BNTIEN
     * created date: 06-08-2023 16:42:33
     */
    openMenu() {
      if (!this.isReadonlyCBB) {
        this.isShowSelectEntity = !this.isShowSelectEntity;
      }
    },

    /**
     * Mô tả: Tìm kiếm item
     * created by : BNTIEN
     * created date: 30-07-2023 08:48:34
     */
    onSelectedEntity(item, index) {
      this.$_MSEmitter.emit("onSelectedEntityCBBSingle", item, this.propId);
      this.indexEntitySelected = index;
    },

    /**
     * Mô tả: Lắng nghe sự thay đổi text trong input search và tìm kiếm trong combobox
     * created by : BNTIEN
     * created date: 06-06-2023 22:31:16
     */
    async onSearchChange($event) {
      try {
        await this.$_MSEmitter.emit("onSearchChangeCBBSingle", $event.target.value, this.propId);
        this.isShowSelectEntity = true;
      } catch {
        return;
      }
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
     * Mô tả: Xử kí keydown lên xuống, enter
     * created by : BNTIEN
     * created date: 06-06-2023 22:02:18
     */
    onKeyDownEntity(event) {
      try {
        if (!this.isReadonlyCBB) {
          const maxLength = this.listEntitySearchCBB.Data.length;
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
                this.$_MSEmitter.emit("onKeyDownEntityCBBSingle", this.indexEntitySelected, this.propId);
                this.isShowSelectEntity = false;
              } else {
                this.isShowSelectEntity = true;
              }
            }
          }
        }
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
        const cbb = this.$refs.cbbSelectSingle;
        const remainingSpace = cbb.scrollHeight - (cbb.scrollTop + cbb.clientHeight);
        const textSearch = this.isShowCode ? this.entityCBB[this.propCode] : this.entityCBB[this.propName];
        // Kiểm tra xem đã scroll đến cuối combobox chưa
        if (remainingSpace <= 10) {
          // Gọi API để lấy thêm dữ liệu
          await this.$_MSEmitter.emit("handleScrollCBBSingle", textSearch, this.propId);
        }
      } catch {
        return;
      }
    },

    /**
     * Mô tả: Xử lí focus vào ô input trong combobox
     * created by : BNTIEN
     * created date: 12-08-2023 00:21:00
     */
    focus() {
      this.$refs["InputCBB"].focus();
    },

    /**
     * Mô tả: xử lí click outside
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
@import url(./ComboboxSelectSingle.css);

input {
  height: calc(26px - 0.667px * 2);
  width: 100%;
  box-sizing: border-box;
  border: unset;
  border-radius: 4px 0 0 4px;
  padding-left: 12px;
}

input:focus {
  border: unset;
}

.border-red {
  border: 1px solid red;
}

#info-payment-detail .border-red {
  border: 1px solid red;
}

#detail-info-provider .cbb-selected,
#info-payment-detail .cbb-selected {
  background-color: var(--color-btn-default);
  color: #ffffff;
}

#info-payment-detail .readonly-input-cbb {
  background-color: #eff0f2 !important;
  border: unset;
}

#info-payment-detail input {
  height: calc(28px - 0.667px * 2);
}

.not-found-entity {
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
