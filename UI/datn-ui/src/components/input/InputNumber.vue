<template>
  <input
    class="textfield"
    type="text"
    :value="modelValue"
    @input="onInput($event)"
    :title="titleContent"
    ref="inputText"
    :maxlength="maxLength"
    v-money="money"
  />
</template>

<script>
import { VMoney } from "v-money";
export default {
  name: "InputNumber",

  props: {
    modelValue: String,
    titleContent: String,
    maxLength: Number,
    isMoney: Boolean,
  },

  data() {
    return {
      money: {
        thousands: ".",
        precision: 0,
      },
    };
  },

  methods: {
    focus() {
      this.$refs.inputText.focus();
    },

    /**
     * Mô tả: Cấm nhập dấu trừ
     * created by : BNTIEN
     * created date: 11-08-2023 13:38:56
     */
    onInput($event) {
      if (this.isMoney) {
        const cleanedValue = $event.target.value.replace(/[^-0-9.]/g, "");
        $event.target.value = cleanedValue;
        this.$emit("update:modelValue", $event.target.value);
      } else {
        const cleanedValue = $event.target.value.replace(/[^0-9.]/g, "");
        $event.target.value = cleanedValue;
        this.$emit("update:modelValue", $event.target.value);
      }
    },
  },

  directives: { money: VMoney },
};
</script>

<style scoped>
@import url(@/css/base/input.css);

input {
  height: 32px;
  width: 100%;
}
</style>
