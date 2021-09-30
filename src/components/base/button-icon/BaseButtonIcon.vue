<template>
    <div class="wrap-button-icon">
        <div class="icon-20 icon-button ic-add" v-if="typeButton == this.$resourceVn.ButtonIconAdd"></div>
        <div class="icon-20 icon-button ic-apply" v-if="typeButton == this.$resourceVn.ButtonIconApply"></div>
        <div class="icon-20 icon-button ic-active-apply" v-if="typeButton == this.$resourceVn.ButtonIconActiveApply"></div>
        <div class="icon-20 icon-button ic-delete" v-if="typeButton == this.$resourceVn.ButtonIconDelete"></div>

        <div class="text-button">{{ buttonName }}</div>
    </div>
</template>
<script>
export default {
    name: "ButtonIcon",
    props:{
        buttonName: {
            type: String,
            default(){ 
                return '';
            }
        },
        typeButton: {
            type: Number,
            default(){
                return 0;
            }
        }
    }
}
</script>
<style scoped>
    @import "../../../css/base/button-icon/base-button-icon.css";
</style>