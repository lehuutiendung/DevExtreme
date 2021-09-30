<template>
    <div class="wrap-tooltip">
        <DxTooltip
        :visible="defaultVisible"
        :close-on-outside-click="true"
        :target="element"
        >
            <div>{{ content }}</div>
        </DxTooltip>
    </div>
</template>
<script>
import { DxTooltip } from 'devextreme-vue/tooltip';
import {EventBus} from "../../../main"
export default {
    name: 'Tooltip',
    components: {
        DxTooltip
    },
    data() {
        return {
            defaultVisible: false,
            element: "",                        //Element hiển thị tooltip
            content: "",                        //Nội dung tooltip
        }
    },
    mounted() {
        /**
         * @description Hiển thị tooltip
         * @created LHTDung
         * @date 28/09/2021
         */
        EventBus.$on('showTooltip', (el, value) => {    
            this.element = el;
            this.content = value;
            this.defaultVisible = true;
        }),

        /**
         * @description Ẩn tooltip
         * @created LHTDung
         * @date 28/09/2021
         */
        EventBus.$on('hideTooltip', () => {
            this.defaultVisible = false;            
        })
    },
}
</script>
<style scoped>
    @import "../../../css/base/tooltip/tooltip.css";
</style>