<template>
    <input type="text" class="wrap-base-input" :style="{width : width}" :tabIndex="tabIndex" v-on="inputListeners">
</template>
<script>
export default {
    name: 'Input',
    props:{
        width: {
            type: String,
            default(){
                return '748px';
            }
        },

        //tabindex
        tabIndex: {
            type: Number,
            default(){
                return 1;
            }
        },
    },
    computed: {
      inputListeners: function () {
        var vm = this
        return Object.assign({},
          this.$listeners,
          {
            input: function (event) {
               vm.$emit('input', event.target.value)
            }
          }
        )
      },
    },
}
</script>
<style scoped>
    @import "../../../css/base/input/base-input.css";
</style>