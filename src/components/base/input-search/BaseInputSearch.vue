<template>
    <div class="div-input-search" @click="showBorderInput()">
        <div class="wrap-icon-input-search">
            <div class="icon-20 icon-input-search"></div>
        </div>
        <input class="input-search" type="text" placeholder="Tìm kiếm" >
    </div>
</template>
<script>
export default {
    name: "InputSearch",
    mounted() {
        document.addEventListener('click', () => {
            document.querySelector('.div-input-search').style.border = "1px solid #e0e0e0";
        })
    },
    methods: {
        /**
         * @description Hiển thị border của Input khi click input
         * @date 25/09/2021
         * @created LHTDung
         */
        showBorderInput(){
            event.stopPropagation();
            document.querySelector('.div-input-search').style.border = "1px solid #34b057";
        }
    },
}
</script>
<style scoped>
    @import "../../../css/base/input-search/base-input-search.css";
</style>