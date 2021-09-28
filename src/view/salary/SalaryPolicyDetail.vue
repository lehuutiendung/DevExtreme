<template>
    <div class="wrap-modal-box" :style="{ display: modalBoxState }">
        <div class="box-info">
            <div class="title-box div-flex">
                <div class="title-wrap">
                    <div class="title">{{ this.$resourceVn.ChooseComponent }}</div>
                </div>
                <div class="menu-wrap">
                    <div class="icon-36 flex-icon exit-modalbox" 
                    @click="clickCancel()" @mouseenter="showTooltip()" @mouseleave="hideTooltip()">
                        <div class="icon-20 exit"></div>
                    </div>
                </div>
            </div>
            <div class="wrap-content">
                <div class="wrap-tool-grid">
                    <InputSearch @keyup.enter.native="handleInputSearch($event)"/>
                    <div class="space"></div>
                    <Dropdown width="230px" 
                    :listData="this.$resourceVn.ComponentTypeDropdown" 
                    :typeDropdown="this.$resourceVn.DropdownStatus"
                    @getValueComponent="getValueComponent"/>
                </div>
                <div class="wrap-content-grid">
                    <DataGrid 
                        :listHeader="listHeader" 
                        :dataSource="dataSourceWithoutCheck" 
                        :modalBoxShow="modalBoxShow"
                        @clickItemDataGrid="clickItemDataGrid($event)"
                    >
                        <template #Status="{ data }">
                            <div>{{ data.Status }}</div>
                        </template>
                    </DataGrid>
                </div>
                <TheFooter class="footer-custome" :totalRecord="totalRecord" :startIndex="startIndex" :endIndex="endIndex"/>
                <div class="space-footer-button"></div>
            </div>
            <div class="flex-wrap-button">
                <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText" @click.native="clickCancel()"/>
                <Button class="button-green" :buttonName="this.$resourceVn.ButtonApplyText" @click.native="clickApply()"/>
            </div>
        </div>
    </div>
</template>
<script>

import fakeData from '../../js/fake-data/data-grid-add-component'

import axios from "axios";

import Button from "../../components/base/button/BaseButton.vue"
import InputSearch from "../../components/base/input-search/BaseInputSearch.vue"
import Dropdown from "../../components/base/dropdown/BaseDropdown.vue"
import DataGrid from "../../components/base/data-grid/BaseDataGrid.vue"
import TheFooter from "../../components/layout/TheFooter.vue"
import { EventBus } from '../../main';

export default {
    name: "ModalBoxSalaryComponent",
    components: {
        InputSearch,
        Dropdown,
        DataGrid,
        TheFooter,
        Button,
    },
    props:{
        // Trạng thái ẩn hiện của modalbox
        modalBoxShow: {
            type: Boolean,
            default(){
                return false;
            }
        },

        //Danh sách chứa các thành phần lương từ SalaryPolicyList
        listChoosedComponent: {
            type: Array,
            default(){
                return []; 
            }
        },

        //Item thành phần sau khi xóa sẽ được cập nhật lại vào dataSourceWithoutCheck
        updateItemSource:{
            type: Object,
            default(){
                return {};
            }
        },

        //Màn hiện tại đang hiển thị
        mode:{
            type: Number,
            default(){
                return 0;
            }
        }
    },
    data() {
        return {
            listHeader: fakeData.listHeader,                //Dữ liệu các tiêu đề của grid Thành phần
            listItem: [],                                   //Danh sách chứa dữ liệu được chọn từ Datagrid Thành phần
            dataSourceWithoutCheck: this.dataSource,        //Dữ liệu chứa các bản ghi chưa được chọn trong bảng Thành phần (dùng để bind lên Table)
            dataSource: [],                                 //Dữ liệu của grid Thành phần
            totalRecord: 0,                                 //Tổng số bản ghi
            totalPage: 0,                                   //Tổng số trang
            startIndex: 0,                                  //Bản ghi bắt đầu
            endIndex: 0,                                    //Bản ghi kết thúc
            filterSearch: "",                               //Lưu key tìm kiếm
            componentType: "",                              //Lưu key lọc theo Loại thành phần
            pageSize: 15,                                   //Kích thước số bản ghi/trang
            pageNumber: 1,                                  //Trang hiện tại
            maxPages: 4,                                    //Max số trang hiển thị
        }
    },
    mounted() {
        /************************************************ Gọi API ************************************************/
        /**
         * @description Gọi API thực hiện phân trang
         * @created LHTDung
         * @date 23/09/2021
         */
        this.calAPIFilterComponent(this.pageSize, this.pageNumber, this.filterSearch, this.componentType)
        
        /**
         * @description Update trang khi click trở về trước
         * @created LHTDung
         * @date 23/09/2021
         */
        EventBus.$on('updatePrevPageComponent', () => {
            console.log('prev');
            this.updatePrevPageNumber();
        })

        /**
         * @description Update trang khi click next trang
         * @created LHTDung
         * @date 23/09/2021
         */
        EventBus.$on('updateNextPageComponent', () => {
            this.updateNextPageNumber();
        })

    },
    computed: {
        /**
         * @description Thay đổi trạng thái của popup
         * @date 18/09/2021
         * @createdBy LHTDung
         */
        modalBoxState() {
            if (this.modalBoxShow) {
                return "flex";
            } else {
                return "none";
            }
        },
    },

    methods: {
        /**
         * @description Xử lý sự kiện click button "Hủy bỏ"
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        clickCancel(){
            this.$emit("exitModalBox");        
        },

        /**
         * @description Bắt sự kiện emit từ DataGrid, nhận dữ liệu các dòng được click
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        clickItemDataGrid(listData){
            this.listItem = listData;
        },

        /**
         * @description Emit danh sách các thành phần được chọn đến SalaryPolicyList để hiển thị ra màn thêm mới
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        clickApply(){
            this.listItem.forEach(itemList => {
                let index = this.dataSourceWithoutCheck.indexOf(itemList);
                if(index > - 1){
                    this.dataSourceWithoutCheck.splice(index, 1);
                }
            });
            console.log(this.dataSource);
            this.$emit('getListComponent', this.listItem);
            this.$emit("exitModalBox"); 
            console.log(this.listItem);       
        },

        /**
         * @description Xử lý phân trang
         * @created LHTDung
         * @date 23/9/2021
         */
        paginate(totalRecord, pageNumber, pageSize, maxPages, totalPage){
            // đảm bảo trang hiện tại không nằm ngoài phạm vi
            if (pageNumber < 1) {
                pageNumber = 1;
            } else if (pageNumber > totalPage) {
                pageNumber = totalPage;
            }

            let startPage, endPage;
            // Trường hợp tổng số trang bé hơn số trang hiển thị tối đa
            if (totalPage <= maxPages) {
                startPage = 1;
                endPage = totalPage;
            } else {
                // tổng số trang nhiều hơn tối đa => tính trang bắt đầu và trang kết thúc
                let maxPagesBeforeCurrentPage = Math.floor(maxPages / 2);
                let maxPagesAfterCurrentPage = Math.ceil(maxPages / 2) - 1;
                if (pageNumber <= maxPagesBeforeCurrentPage) {
                    // trang hiện tại gần đầu
                    startPage = 1;
                    endPage = maxPages;
                } else if (pageNumber + maxPagesAfterCurrentPage >= totalPage) {
                    // trang hiện tại gần cuối
                    startPage = totalPage - maxPages + 1;
                    endPage = totalPage;
                } else {
                    // trang hiện tại nằm ở vùng giữa
                    startPage = pageNumber - maxPagesBeforeCurrentPage;
                    endPage = pageNumber + maxPagesAfterCurrentPage;
                }
            }

            // tính chỉ số của bản ghi bắt đầu và kết thúc
            let startIndex = (pageNumber - 1) * pageSize;
            let endIndex = Math.min(startIndex + pageSize - 1, totalRecord - 1);

            // tạo một mảng các trang
            let pages = Array.from(Array((endPage + 1) - startPage).keys()).map(i => startPage + i);
            var obj = {
                totalRecord: totalRecord,
                pageNumber: pageNumber,
                pageSize: pageSize,
                totalPage: totalPage,
                startPage: startPage,
                endPage: endPage,
                startIndex: startIndex,
                endIndex: endIndex,
                pages: pages
            }
            return obj;
        },

        /**
         * @description Gọi API thực hiện phân trang, tìm kiếm (Chính sách)
         */
        calAPIFilterComponent(pageSize, pageNumber, filter, componentType){
            axios.get(`https://localhost:44330/api/Components/FilterComponent?pageSize=${pageSize}&pageNumber=${pageNumber}&filter=${filter}&componentType=${componentType}`)
            .then(res => {
                this.totalPage = res.data.TotalPage;
                this.totalRecord = res.data.TotalRecord;
                this.dataSource = res.data.Data;
                this.dataSourceWithoutCheck = res.data.Data;
                let objPage = this.paginate(this.totalRecord, this.pageNumber, this.pageSize, this.maxPages, this.totalPage);
                this.startIndex = objPage.startIndex;
                this.endIndex = objPage.endIndex;
            })
            .catch(err => {
                console.error(err); 
            })
        },

        /**
         * @description Cập nhật trang khi click trở về trước 1 trang
         * @created LHTDung
         * @date 23/09/2021
         */
        updatePrevPageNumber(){
            if(this.pageNumber > 1){
                this.pageNumber--;
            }else{
                this.pageNumber = 1;
            }
        },

        /**
         * @description Cập nhật trang khi click next 1 trang
         * @created LHTDung
         * @date 23/09/2021
         */
        updateNextPageNumber(){
            if(this.pageNumber < this.totalPage){
                this.pageNumber++;
            }else{
                this.pageNumber = this.totalPage;
            }
        },

        /**
         * @description Xử lý sự kiện tìm kiếm Chính sách (Input)
         * @created LHTDung
         * @date 23/09/2021
         */
        handleInputSearch(e){
            this.filterSearch = e.target.value;
            this.pageNumber = 1;
            this.calAPIFilterComponent(this.pageSize, this.pageNumber, this.filterSearch, this.componentType)
        },

        /**
         * @description Nhận giá trị từ dropdown filter thành phần
         * @date 25/09/2021
         * @created LHTDung
         */
        getValueComponent(value){
            if(value == this.$resourceVn.ComponentTypeDropdown[0].Text){
                this.componentType = "";
            }else{
                this.componentType = value;
            }
            console.log(value);
        },

        /**
         * @description Hiển thị tooltip
         * @created LHTDung
         * @date 28/09/2021
         */
        showTooltip(){
            EventBus.$emit('showTooltip', '.exit-modalbox' , this.$resourceVn.ESC);
        },

        /**
         * @description Ẩn tooltip
         * @created LHTDung
         * @date 28/09/2021
         */
        hideTooltip(){
            EventBus.$emit('hideTooltip');
        },
    },

    watch:{
        /**
         * @description Khi có sự thay đổi của updateItemSource (có Thành phần Lương được xóa) -> Update item vào danh sách dataSourceWithoutCheck
         * @date 22/9/2021
         * @created LHTDung
         */
        updateItemSource: function(){
            this.dataSourceWithoutCheck.unshift(this.updateItemSource);
        },

        /**
         * @description Khi có sự thay đổi về số bản ghi/trang -> Gọi API phân trang, trở về trang đầu tiên
         * @created LHTDung
         * @date 23/09/2021
         */
        pageSize: function(){
            this.pageNumber = 1;
            this.calAPIFilterComponent(this.pageSize, this.pageNumber, this.filterSearch, this.componentType)
        },

        /**
         * @description Khi có sự thay đổi về trang -> Gọi API phân trang
         * @created LHTDung
         * @date 23/09/2021
         */
        pageNumber: function(){
            this.calAPIFilterComponent(this.pageSize, this.pageNumber, this.filterSearch, this.componentType)
        },

        componentType: function(){
            this.calAPIFilterComponent(this.pageSize, this.pageNumber, this.filterSearch, this.componentType)
        },

        /**
         * @description Khi thay đổi màn làm việc, cập nhật lại dữ liệu đổ lên Datagrid
         * @created LHTDung
         * @date 23/09/2021
         */
        mode: function(){
            this.pageNumber = 1;
            if(this.mode == this.$resourceVn.EditScreen || this.mode == this.$resourceVn.AddScreen){
                this.calAPIFilterComponent(this.pageSize, this.pageNumber, this.filterSearch, this.componentType)
            }
            if(this.mode == this.$resourceVn.MainScreen){
                this.listItem = [];
                
            }
        },

        /**
         * @description Khi sửa dữ liệu, đồng bộ listChoosedComponent từ SalaryPolicyList với listItem 
         * @created LHTDung
         * @date 23/09/2021
         */
        listChoosedComponent: function(){
            // this.listItem = this.listChoosedComponent;
            // let checkedComponent = [];
            // this.listChoosedComponent.forEach(item => {
            //     item.ComponentValue = "";
            //     checkedComponent.push(item);
            // });
            // console.log(checkedComponent);

            // checkedComponent.forEach(element => {
            //     let index = this.dataSourceWithoutCheck.indexOf(element);
            //     this.dataSourceWithoutCheck.splice(index, 1);
            // });
            // console.log(this.dataSourceWithoutCheck);
        }
    }
}
</script>
<style scoped>
    @import "../../css/layout/modalbox-salary.css";
</style>