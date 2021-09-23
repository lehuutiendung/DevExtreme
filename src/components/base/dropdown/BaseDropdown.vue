<template>
    <div class="wrap-dropdown" v-click-outside="hideDropdown">
        <div class="dropdown-content border-dropdown" @click="showDropdownData()">
            <div class="dropdown-text">
                {{ valueOfDropdown }}
            </div>
            <div class="icon-20 icon-svg dropdown-button"></div>
        </div>
        <div class="wrap-dropdown-data status-dropdown"
        :class="{'status-dropdown': typeOfStatus, 'paging-dropdown' : typeOfPaging}"
        :style="{width: width}"
        v-if="showData == true"
        >
            <ul class="dropdown-data-list">
                <li class="item" 
                :class="{'choose-item' : index == current?true:false}"
                v-for="(item, index) in listData" 
                :key="index"
                @click="chooseItem(item.Text ,index)"
                >
                    <div class="item-text">{{ item.Text }}</div>
                    <div class="icon-20 icon-svg" 
                    :class="{'icon-tick' : index == current?true:false}">
                    </div>
                </li>
            </ul>
        </div>
  </div>
</template>
<script>

import {EventBus} from "../../../main"
import ClickOutSide from "../../../js/detectoutside"
export default {
    name: "Dropdown",
    directives: {
        ClickOutSide
    },
    props: {
        listData: {
            type: Array,
            default(){
                return [];
            },
        },

        width: {
            type: String,
            default(){
                return '230px'; 
            }
        },
        
        typeDropdown: {
            type: Number,
            default(){
                return 0;
            }
        }
    },
    data() {
        return {
            showData: false, //Mặc định ẩn,
            current: 0, //Item đã click
            valueOfDropdown: "",
        }
    },
    created() {
        this.valueOfDropdown = this.listData[0].Text;
    },
    computed: {
        /**
         * @description Kiểm tra kiểu của dropdown để bind class tương ứng (Loại dropdown "tất cả trạng thái")
         * @date 17/09/2021
         * @createBy LHTDung
         */
        typeOfStatus(){
            if(this.typeDropdown == this.$resourceVn.DropdownStatus){
                return true;
            }else return false;
        },

        /**
         * @description Kiểm tra kiểu của dropdown để bind class tương ứng (Loại dropdown "Phân trang")
         * @date 17/09/2021
         * @createBy LHTDung
         */
        typeOfPaging(){
            if(this.typeDropdown == this.$resourceVn.DropdownPaging){
                return true;
            }else return false;
        }
    },

    methods: {
        /**
         * @description Click ẩn hiện danh sách item của dropdown
         * @date 17/09/2021
         * @createBy LHTDung
         */
        showDropdownData(){
            this.showData = !this.showData;
        },
        
        /**
         * @description Click ra ngoài dropdown -> Ẩn danh sách item của dropdown
         * @date 17/09/2021
         * @createBy LHTDung
         */
        hideDropdown(){
            this.showData = false;
        },

        /**
         * @description Click chọn item
         * @date 17/09/2021
         * @createBy LHTDung
         */
        chooseItem(value, index){
            this.valueOfDropdown = value;
            this.current = index;
            this.showData = false;
            EventBus.$emit('getValueFromDropdownCustomize', this.valueOfDropdown);
        }

    },
}
</script>
<style scoped>
    @import "../../../css/base/dropdown/base-dropdown.css";
</style>