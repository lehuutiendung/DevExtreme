<template>
    <div class="wrap-button">
        <div class="text-button">{{ buttonName }}</div>
    </div>
</template>
<script>
export default {
    name: "Button",
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
    @import "../../../css/base/button/base-button.css";
</style>