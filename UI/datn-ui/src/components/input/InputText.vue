<template>
  <input
    class="textfield"
    :type="typeValue"
    :value="modelValue"
    @input="onInput($event)"
    :title="titleContent"
    ref="inputText"
    :maxlength="maxLength"
    :disabled="disabled"
  />
</template>

<script>
export default {
  name: "InputText",
  props: {
    modelValue: String,
    titleContent: String,
    maxLength: Number,
    typeValue: {
      type: String,
      default: "text",
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
    focus() {
      this.$refs.inputText.focus();
    },

    onInput($event) {
      this.$emit("update:modelValue", $event.target.value);
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/input.css);

input {
  height: 32px;
  width: 100%;
}
</style>
