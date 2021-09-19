<template>
    <div class="div-input-search" :tabindex="0">
        <div class="wrap-icon-input-search">
            <div class="icon-20 icon-input-search"></div>
        </div>
        <input class="input-search" type="text" placeholder="Tìm kiếm" >
    </div>
</template>
<script>
export default {
    name: "InputSearch",
}
</script>
<style scoped>
    @import "../../../css/base/input-search/base-input-search.css";
</style>