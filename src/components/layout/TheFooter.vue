<template>
    <div class="footer-table">
        <div class="footer-paginate">
            <div class="total-record">
                <div>Tổng số bản ghi: <b>10</b></div>
            </div>
            <div class="paginate-wrap">
                <div class="text">Số bản ghi/trang</div>
                <Dropdown width="80px" :listData="this.$resourceVn.PagingNumber" :typeDropdown="this.$resourceVn.DropdownPaging"/>
                <div class="range-paging"><span>{{0}} - {{0}}</span> bản ghi</div>
                <div class="number-page-wrap">
                    <div class="icon-svg icon-20 prev-page"></div>
                    <div class="icon-svg icon-20 next-page"></div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Dropdown from "../base/dropdown/BaseDropdown.vue"
export default {
    name: "TheFooter",
    components: {
        Dropdown,
    },
}
</script>
<style scoped>
    @import "../../css/layout/footer.css";
</style>
    