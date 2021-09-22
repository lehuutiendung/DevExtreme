<template>
    <input type="text" class="wrap-base-input" :style="{width : width + '!important'}" v-on="inputListeners">
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
        }
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