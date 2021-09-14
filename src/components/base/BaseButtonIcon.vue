<template>
    <div class="wrap-button-icon">
        <div class="icon-20 icon-button"></div>
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
        }
    }
}
</script>
<style scoped>
    @import "../../css/base/base-button-icon.css";
</style>