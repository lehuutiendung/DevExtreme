<template>
    <div class="footer-table">
        <div class="footer-paginate">
            <div class="total-record">
                <div>Tổng số bản ghi: <b>{{ totalRecord }}</b></div>
            </div>
            <div class="paginate-wrap">
                <div class="text">Số bản ghi/trang</div>
                <Dropdown width="80px" :listData="this.$resourceVn.PagingNumber" :typeDropdown="this.$resourceVn.DropdownPaging"/>
                <div class="range-paging"><span>{{ startIndex + 1 }} - {{ endIndex + 1 }}</span> bản ghi</div>
                <div class="number-page-wrap">
                    <div class="icon-svg icon-20 prev-page" @click="prevPage()"></div>
                    <div class="icon-svg icon-20 next-page" @click="nextPage()"></div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import {EventBus} from "../../main"
import Dropdown from "../base/dropdown/BaseDropdown.vue"
export default {
    name: "TheFooter",
    components: {
        Dropdown,
    },
    props:{
        // Tổng số bản ghi
        totalRecord:{
            type: Number,
            default(){
                return 0;
            }
        },

        // Bản ghi bắt đầu trong trang
        startIndex:{
            type: Number,
            default(){
                return 0;
            }
        },

        // Bản ghi kết thúc trong trang
        endIndex: {
            type: Number,
            default(){
                return 0;
            }
        }
    },
    methods: {
        /**
         * @description Click trở về trước 1 trang
         * @created LHTDung
         * @date 23/09/2021
         */
        prevPage(){
            //Update trang DataGrid màn chính
            this.$emit('updatePrevPageNumber');
            //Update trang DataGrid màn chọn thành phần
            EventBus.$emit('updatePrevPageComponent');
        },

        /**
         * @description Click next 1 trang
         * @created LHTDung
         * @date 23/09/2021
         */
        nextPage(){
            //Update trang DataGrid màn chính
            this.$emit('updateNextPageNumber');
            //Update trang DataGrid màn chọn thành phần
            EventBus.$emit('updateNextPageComponent');
        }
    },
}
</script>
<style scoped>
    @import "../../css/layout/footer.css";
</style>
    